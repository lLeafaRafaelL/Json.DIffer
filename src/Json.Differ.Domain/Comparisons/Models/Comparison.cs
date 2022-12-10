using Json.Differ.Core.Models;
using Json.Differ.Domain.Files.Models;
using Newtonsoft.Json.Linq;
using Json.Differ.Domain.Files.Enums;

namespace Json.Differ.Domain.Comparisons.Models
{
    public class Comparison : AgregateRoot
    {
        private Comparison()
        {
            _filesDiffs = new List<ComparisonFileDiff>();
            _files = new List<ComparisonFile>();
        }

        public Comparison(Guid externalId, IList<FileToCompare> files) : this()
        {
            ExternalId = externalId;
            AddFiles(files);
            CompareFilesDiffs(files);
        }

        public virtual Guid ExternalId { get; private set; }
        public virtual string? Result { get; private set; }

        private List<ComparisonFile> _files;
        public virtual IReadOnlyList<ComparisonFile> Files { get { return _files; } }

        private List<ComparisonFileDiff> _filesDiffs;
        public virtual IReadOnlyList<ComparisonFileDiff> FilesDiffs { get { return _filesDiffs; } }
        public virtual IReadOnlyList<ComparisonFileDiff> LeftFileDiffs => FilesDiffs.Where(a => a.IsLeftSide).ToList();
        public virtual IReadOnlyList<ComparisonFileDiff> RightFileDiffs => FilesDiffs.Where(a => a.IsRightSide).ToList();

        private void AddFiles(IList<FileToCompare> files)
        {
            if (files == null)
                throw new ArgumentNullException($"{files} must be provided");

            foreach (var file in files)
                _files.Add(new ComparisonFile(Id, file.Id));
        }

        private void AddFileDiff(FileSide fileSide, string field, string value) => _filesDiffs.Add(new ComparisonFileDiff(Id, fileSide, field, value));

        private void CompareFilesDiffs(IList<FileToCompare> files)
        {
            var leftFile = files.FirstOrDefault(a => a.IsLeftSide);
            var rightFile = files.FirstOrDefault(a => a.IsRightSide);

            var jsonLeftFile = JObject.Parse(leftFile.Data);
            var jsonRightFile = JObject.Parse(rightFile.Data);

            if(JToken.DeepEquals(jsonLeftFile, jsonRightFile))
                Result = "The files are equals";
            else
            {
                Result = "The files are differents";

                var leftFileProperties = jsonLeftFile.Properties().ToList();
                var rightFileProperties = jsonRightFile.Properties().ToList();

                leftFileProperties?.Where(a => !rightFileProperties.Any(b => b.Path == a.Path) || rightFileProperties.Any(c => c.Path == a.Path && !c.Value.ToString().Equals(a.Value.ToString())))
                                  ?.Select(d => (d.Path, d.Value))
                                  ?.ToList()
                                  ?.ForEach(item => {
                                      AddFileDiff(FileSide.LEFT, item.Path, item.Value.ToString());
                                  });

                rightFileProperties?.Where(a => !leftFileProperties.Any(b => b.Path == a.Path) || leftFileProperties.Any(c => c.Path == a.Path && !c.Value.ToString().Equals(a.Value.ToString())))
                                   ?.Select(d => (d.Path, d.Value))
                                   ?.ToList()
                                   ?.ForEach(item => {
                                       AddFileDiff(FileSide.RIGHT, item.Path, item.Value.ToString());
                                   });
            }
        }
    }
}