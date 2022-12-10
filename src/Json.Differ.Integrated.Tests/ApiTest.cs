using FluentAssertions;
using Json.Differ.Integrated.Tests.Utils;
using JsonDiffer.Api.Controllers.Files;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Json.Differ.Integrated.Tests
{
    [TestCaseOrderer("Json.Differ.Integrated.Tests", "Api.Tests")]
    public class ApiTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private const string baseUri = "json-differ/v1/diff";
        private Guid ExternalId => Guid.NewGuid();

        public ApiTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Upload json encoded left file"), TestPriority(1)]
        public async Task Post_UploadLeftFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}/left";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Upload json encoded right file wich the externalId is already used"), TestPriority(2)]
        public async Task Post_UploadLeftFile_WithExternalIdAlreadyUsed_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}/left";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded right file"), TestPriority(3)]
        public async Task Post_UploadRightFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}/right";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Upload json encoded right file wich the externalId is already used"), TestPriority(4)]
        public async Task Post_UploadRightFile_WithExternalIdAlreadyUsed_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}/right";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


        [Fact(DisplayName = "Compare the uploaded json files"), TestPriority(5)]
        public async Task Post_CompareFiles_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Try to compare the uploaded json files wich i was already compared"), TestPriority(6)]
        public async Task Post_CompareFiles_AreadyCompared_ResultShouldBeOK()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{ExternalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}