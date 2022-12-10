using FluentAssertions;
using Json.Differ.Application.Files.UploadFileToCompare;
using Json.Differ.Core.Data;
using Json.Differ.Domain.Files.Factories.Impl;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Repositories;
using Moq;
using System.Net;

namespace Json.Differ.Unit.Tests.Application.Files.UploadFileToCompare
{
    public class UploadFileToCompareHandlerTest : IClassFixture<UploadFileToCompareHandlerFixture>
    {
        private readonly UploadFileToCompareHandlerFixture _fixture;

        public UploadFileToCompareHandlerTest(UploadFileToCompareHandlerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Upload a json file")]
        public async Task UploadFileToCompareHandler_Upload_ShouldReturnCreated()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = _fixture.CreateRequest();
            var repository = new Mock<IFileRepository>();
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new UploadFileToCompareHandler(
                repository.Object,
                new FileFactory(repository.Object),
                unitOfWork.Object,
                new UploadFileToCompareValidator());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<FileToCompare>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            result.Status.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Upload request without a file")]
        public async Task UploadFileToCompareHandler_UploadWithoutAFile_ShouldReturnBadRequest()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = _fixture.CreateRequest();
            request.EncodedFile = null;
            var repository = new Mock<IFileRepository>();
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new UploadFileToCompareHandler(
                repository.Object,
                new FileFactory(repository.Object),
                unitOfWork.Object,
                new UploadFileToCompareValidator());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<FileToCompare>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            result.Status.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Upload request with a encoded not json file")]
        public async Task UploadFileToCompareHandler_UploadNotJsonFile_ShouldReturnBadRequest()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = _fixture.CreateRequest("dGVzdCBhIG5vdCBqc29uIGZpbGU=");
            var repository = new Mock<IFileRepository>();
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new UploadFileToCompareHandler(
                repository.Object,
                new FileFactory(repository.Object),
                unitOfWork.Object,
                new UploadFileToCompareValidator());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<FileToCompare>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            result.Status.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
