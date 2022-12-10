using FluentAssertions;
using Json.Differ.Integrated.Tests.Utils;
using JsonDiffer.Api.Controllers.Files;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Json.Differ.Integrated.Tests
{
    public class ApiTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string baseUri = "json-differ/v1/diff/";
        private Guid _externalId = Guid.NewGuid();

        public ApiTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_UploadLeftFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}/left";
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

        [Fact]
        public async Task Post_UploadRightFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}/right";
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

        [Fact]
        public async Task Post_UploadRightFile_WithExternalIdAlreadyUsed_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}/right";
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

        [Fact]
        public async Task Post_CompareFiles_WithExternalIdAlreadyUsed_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_CompareFiles_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }   

        [Fact]
        public async Task Post_CompareFiles_AreadyCompared_ResultShouldBeOK()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_externalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}