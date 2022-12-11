using FluentAssertions;
using Json.Differ.Integrated.Tests.Configurations;
using Json.Differ.Integrated.Tests.Utils;
using JsonDiffer.Api.Controllers.Files;

namespace Json.Differ.Integrated.Tests.JsonDifferApi
{
    [TestCaseOrderer("Json.Differ.Integrated.Tests.Configurations.PriorityOrderer", "Json.Differ.Integrated.Tests")]
    public class JsonDIfferApiTest : IClassFixture<CustomWebApplicationFactory<Program>>, IClassFixture<JsonDifferApiTestFixture>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private const string baseUri = "json-differ/v1/diff";
        private readonly JsonDifferApiTestFixture _fixture;
        public JsonDIfferApiTest(CustomWebApplicationFactory<Program> factory, JsonDifferApiTestFixture fixture)
        {
            _factory = factory;
            _fixture = fixture;
        }

        [Fact(DisplayName = "Upload json encoded left file"), TestPriority(1)]
        public async Task Post_UploadLeftFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/left";
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
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/left";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded right file"), TestPriority(3)]
        public async Task Post_UploadRightFile_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/right";
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

        [Fact(DisplayName = "Upload json encoded right file with a invalid json")]
        public async Task Post_UploadRightFileWithInvalidJson_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/right";
            var request = new FileToUploadDto
            {
                EncodedFile = "YWJjMTIzMjFmYXNk"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded right file with a decoded string")]
        public async Task Post_UploadRightFileWithDecodedString_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/right";
            var request = new FileToUploadDto
            {
                EncodedFile = "abc12345"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded left file with a invalid json")]
        public async Task Post_UploadLeftFileWithInvalidJson_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/left";
            var request = new FileToUploadDto
            {
                EncodedFile = "YWJjMTIzMjFmYXNk"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded left file with a decoded string")]
        public async Task Post_UploadLeftFileWithDecodedString_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/left";
            var request = new FileToUploadDto
            {
                EncodedFile = "abc12345"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload json encoded right file wich the externalId is already used"), TestPriority(4)]
        public async Task Post_UploadRightFile_WithExternalIdAlreadyUsed_ResultShouldBeBadRequest()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}/right";
            var request = new FileToUploadDto
            {
                EncodedFile = "eyAiVGVzdE5hbWUiIDogIkZpbGVzIGFyZSBkaWZmZXJlbnRzIiwgIlByb3BlcnR5MiIgOiAiWFlaICJ9"
            };

            //Act
            var response = await client.PostAsync(endpoint, request.ToHttpStringContent());

            //Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }


        [Fact(DisplayName = "Compare the uploaded json files"), TestPriority(5)]
        public async Task Post_CompareFiles_ResultShouldBeCreated()
        {
            //Arrange
            var client = _factory.CreateClient();
            var endpoint = $"{baseUri}/{_fixture.ExternalId}";

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
            var endpoint = $"{baseUri}/{_fixture.ExternalId}";

            //Act
            var response = await client.PostAsync(endpoint, null);

            //Assert

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}