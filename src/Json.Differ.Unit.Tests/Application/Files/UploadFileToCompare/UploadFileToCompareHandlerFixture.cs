using Bogus;
using Json.Differ.Application.Files.UploadFileToCompare;
using Json.Differ.Domain.Files.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Unit.Tests.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareHandlerFixture
    {

        public UploadFileToCompareRequestDto CreateRequest(string encodedFile = null)
        {
            var requestFaker = new Faker<UploadFileToCompareRequestDto>("pt_BR")
                .CustomInstantiator(a => new UploadFileToCompareRequestDto
                {
                    ExternalId = Guid.NewGuid(),
                    EncodedFile = encodedFile ?? "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9",
                    Side = a.PickRandom<FileSide>()
                });


            return requestFaker.Generate(1).FirstOrDefault();
        }

    }
}