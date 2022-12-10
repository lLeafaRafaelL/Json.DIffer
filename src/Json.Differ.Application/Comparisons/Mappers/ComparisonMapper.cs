using Json.Differ.Application.Files.CompareFilesDiffs;
using Json.Differ.Core.Mapping;
using Json.Differ.Domain.Comparisons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Application.Comparisons
{
    public class ComparisonMapper : IMapper<Comparison, ComparisonDto>
    {
        public void Map(Comparison source, ComparisonDto target) 
            => Map(source);

        public ComparisonDto Map(Comparison source)
        {
            if (source == null)
                return null;

            return new ComparisonDto
            {
                ExternalId = source.ExternalId,
                Result = source.Result,
                CreatedOn = source.CreatedOn,
                UpdatedOn = source.UpdatedOn,
                LeftFileDiffs = source.LeftFileDiffs?.Select(a => 
                    new ComparisonFileDiffDto 
                    { 
                        Field = a.Field,
                        Value = a.Value
                    })?.ToList(),
                RightFileDiffs = source.RightFileDiffs?.Select(a =>
                    new ComparisonFileDiffDto
                    {
                        Field = a.Field,
                        Value = a.Value
                    })?.ToList()
            };
        }
    }
}