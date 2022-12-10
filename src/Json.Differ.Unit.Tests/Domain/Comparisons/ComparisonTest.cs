using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Files.Models;
using FluentAssertions;
using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Params;

namespace Json.Differ.Unit.Tests.Domain.Comparisons
{
    public class ComparisonTest
    {
        public ComparisonTest()
        {

        }

        [Fact]
        public void Comparsion_CompareFiles_ResultShouldBeFilesAreEquals()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFile = @"{         ""description"": ""The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters."",         ""code"": ""generic""     }";
            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFile,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFile,
                        Side = FileSide.RIGHT
                    }),
            };

            //Act

            var comparison = new Comparison(externalId, filesToCompare);

            //Assert

            comparison.Result.Should().Be("The files are equals");
            comparison.Files.Should().HaveCount(2);
            comparison.FilesDiffs.Should().HaveCount(0);
        }

        [Fact]
        public void Comparsion_CompareFiles_ResultShouldBeFilesAreDifferents()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";
            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };

            //Act

            var comparison = new Comparison(externalId, filesToCompare);

            //Assert

            comparison.Result.Should().Be("The files are differents");
            comparison.Files.Should().HaveCount(2);
            comparison.FilesDiffs.Should().HaveCount(1);
            comparison.LeftFileDiffs.Should().HaveCount(1);
        }

        [Fact]
        public void Comparsion_CompareFilesWithSamePropertiesAndDifferentValues_ResultShouldBeFilesAreDifferents()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileLeft = @"{ ""TestName"" : ""Files are equals"" }";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";
            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };

            //Act

            var comparison = new Comparison(externalId, filesToCompare);

            //Assert

            comparison.Result.Should().Be("The files are differents");
            comparison.Files.Should().HaveCount(2);
            comparison.FilesDiffs.Should().HaveCount(2);
            comparison.LeftFileDiffs.Should().HaveCount(1);
            comparison.LeftFileDiffs.Should().HaveCount(1);
        }

        [Fact]
        public void Comparsion_CompareFiles_NullParameter_ShouldThrowsException()
        {
            //Arrange

            var externalId = Guid.NewGuid();

            //Act
            //Assert

            Assert.Throws<ArgumentNullException>(() => new Comparison(externalId, null));

        }
    }
}