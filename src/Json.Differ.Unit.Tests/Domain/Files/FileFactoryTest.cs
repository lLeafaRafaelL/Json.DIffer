using FluentAssertions;
using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Factories.Impl;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Domain.Files.Repositories;
using Moq;
using System.Linq.Expressions;

namespace Json.Differ.Unit.Tests.Domain.Files
{
    public class FileFactoryTest
    {
        [Fact]
        public async Task FileFactory_Create_ReturnAFileToCompareEntity()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var decodedFile = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var cancellationToken = new CancellationTokenSource().Token;

            var fileParam = new FileToCompareFactoryParam
            {
                ExternalId = externalId,
                DecodedFile = decodedFile,
                Side = FileSide.LEFT
            };

            var repository = new Mock<IFileRepository>();

            var factory = new FileFactory(repository.Object);

            //Act

            var fileResult = await factory.CreateAsync(fileParam, cancellationToken);

            //Assert

            fileResult.Succeeded.Should().BeTrue();
            fileResult.Errors.Should().BeEmpty();
            fileResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task FileFactory_InvalidJsonObject_ReturnNotificationError()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var decodedFile = @"abc";
            var cancellationToken = new CancellationTokenSource().Token;

            var fileParam = new FileToCompareFactoryParam
            {
                ExternalId = externalId,
                DecodedFile = decodedFile,
                Side = FileSide.LEFT
            };

            var repository = new Mock<IFileRepository>();

            var factory = new FileFactory(repository.Object);

            //Act

            var fileResult = await factory.CreateAsync(fileParam, cancellationToken);

            //Assert

            fileResult.Succeeded.Should().BeFalse();
            fileResult.Errors.Count().Should().BeGreaterThan(0);
            fileResult.Value.Should().BeNull();
        }

        [Fact]
        public async Task FileFactory_ExistesFile_ReturnNotificationError()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var decodedFile = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var cancellationToken = new CancellationTokenSource().Token;

            var fileParam = new FileToCompareFactoryParam
            {
                ExternalId = externalId,
                DecodedFile = decodedFile,
                Side = FileSide.LEFT
            };

            var repository = new Mock<IFileRepository>();
            repository.Setup(a => a.GetAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new FileToCompare(fileParam));

            var factory = new FileFactory(repository.Object);

            //Act

            var fileResult = await factory.CreateAsync(fileParam, cancellationToken);

            //Assert

            fileResult.Succeeded.Should().BeFalse();
            fileResult.Errors.Count().Should().BeGreaterThan(0);
            fileResult.Value.Should().BeNull();
        }
    }
}