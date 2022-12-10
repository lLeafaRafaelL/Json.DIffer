using FluentAssertions;
using Json.Differ.Application.Comparisons;
using Json.Differ.Application.Files.CompareFilesDiffs;
using Json.Differ.Core.Data;
using Json.Differ.Domain.Comparisons.Factories.Impl;
using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Comparisons.Repositories;
using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Domain.Files.Repositories;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Json.Differ.Unit.Tests.Application.Comparisons.CompareFilesDiffs
{
    public class CompareFilesDiffsHandlerTest
    {
        public CompareFilesDiffsHandlerTest()
        {

        }

        [Fact(DisplayName = "Compare json files")]
        public async Task CompareFilesDiffsHandler_CreateAComparison_ShouldReturnCreated()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = new CompareFilesDiffsRequestDto { ExternalId = Guid.NewGuid() };
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();

            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";

            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };

            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new CompareFilesDiffsHandler(
                repository.Object,
                new ComparisonFactory(repository.Object, fileRepository.Object),
                unitOfWork.Object,
                new CompareFilesDiffsRequestValidator(), 
                new ComparisonMapper());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<Comparison>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Once);
            result.Status.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Compare json files request with a emtpy guid external id")]
        public async Task CompareFilesDiffsHandler_RequestWithEmptyExternalId_ShouldReturnCreated()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = new CompareFilesDiffsRequestDto { ExternalId = Guid.Empty };
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();

            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";

            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };

            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new CompareFilesDiffsHandler(
                repository.Object,
                new ComparisonFactory(repository.Object, fileRepository.Object),
                unitOfWork.Object,
                new CompareFilesDiffsRequestValidator(),
                new ComparisonMapper());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<Comparison>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            result.Status.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Compare json files wich already exists a comparison for thoses files")]
        public async Task CompareFilesDiffsHandler_AlreadyExistsAComprarison_ShouldReturnOK()
        {
            // Arrange

            var cancellationToken = new CancellationTokenSource().Token;

            var request = new CompareFilesDiffsRequestDto { ExternalId = Guid.NewGuid() };
            var unitOfWork = new Mock<IUnityOfWorkAsync>();
            var unitOfWorktransaction = new Mock<IUnityOfWorkAsyncTransaction>();

            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";

            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
               new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = request.ExternalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };

            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            repository.Setup(a => a.GetAsync(It.IsAny<Expression<Func<Comparison, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new Comparison(request.ExternalId, filesToCompare));

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);


            unitOfWork.Setup(a => a.BeginTransactionAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(unitOfWorktransaction.Object);

            var handler = new CompareFilesDiffsHandler(
                repository.Object,
                new ComparisonFactory(repository.Object, fileRepository.Object),
                unitOfWork.Object,
                new CompareFilesDiffsRequestValidator(),
                new ComparisonMapper());

            // Act

            var result = await handler.Handle(request, cancellationToken);

            //Assert

            repository.Verify(a => a.AddAsync(It.IsAny<Comparison>(), It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWork.Verify(a => a.SaveChangesAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            unitOfWorktransaction.Verify(a => a.CommitAsync(It.Is<CancellationToken>(b => b == cancellationToken)), Times.Never);
            result.Status.Should().Be(HttpStatusCode.OK);
        }
    }
}