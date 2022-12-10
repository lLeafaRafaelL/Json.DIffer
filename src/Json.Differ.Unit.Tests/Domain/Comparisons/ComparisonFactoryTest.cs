using FluentAssertions;
using Json.Differ.Domain.Comparisons.Factories.Impl;
using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Domain.Comparisons.Repositories;
using Json.Differ.Domain.Files.Enums;
using Json.Differ.Domain.Files.Models;
using Json.Differ.Domain.Files.Params;
using Json.Differ.Domain.Files.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Json.Differ.Unit.Tests.Domain.Comparisons
{
    public class ComparisonFactoryTest
    {
        [Fact]
        public async Task ComparsionFactory_Create_ReturnAComparisonEntity()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";
            var cancellationToken = new CancellationTokenSource().Token;

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

            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);

            var factory = new ComparisonFactory(repository.Object, fileRepository.Object);

            //Act

            var comparisonResult = await factory.CreateAsync(externalId, cancellationToken);

            //Assert

            comparisonResult.Succeeded.Should().BeTrue();
            comparisonResult.Errors.Should().BeEmpty();
            comparisonResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task ComparsionFactory_ComparisonAlreadyExists_ReturnNotificationError()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";
            var cancellationToken = new CancellationTokenSource().Token;

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

            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            repository.Setup(a => a.GetAsync(It.IsAny<Expression<Func<Comparison, bool>>>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new Comparison(externalId, filesToCompare));

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);

            var factory = new ComparisonFactory(repository.Object, fileRepository.Object);

            //Act

            var comparisonResult = await factory.CreateAsync(externalId, cancellationToken);

            //Assert

            comparisonResult.Succeeded.Should().BeFalse();
            comparisonResult.Errors.Count().Should().BeGreaterThan(0);
            comparisonResult.Value.Should().BeNull();
        }

        [Fact]
        public async Task ComparsionFactory_MissingLeftFile_ReturnNotificationError()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileRight = @"{ ""TestName"" : ""Files are differents"" }";
            var cancellationToken = new CancellationTokenSource().Token;

            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileRight,
                        Side = FileSide.RIGHT
                    }),
            };


            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);

            var factory = new ComparisonFactory(repository.Object, fileRepository.Object);

            //Act

            var comparisonResult = await factory.CreateAsync(externalId, cancellationToken);

            //Assert

            comparisonResult.Succeeded.Should().BeFalse();
            comparisonResult.Errors.Count().Should().BeGreaterThan(0);
            comparisonResult.Value.Should().BeNull();
        }

        [Fact]
        public async Task ComparsionFactory_MissingRightFile_ReturnNotificationError()
        {
            //Arrange

            var externalId = Guid.NewGuid();
            var jsonFileLeft = @"{ ""TestName"" : ""Files are differents"", ""Property2"" : ""XYZ ""}";
            var cancellationToken = new CancellationTokenSource().Token;

            var filesToCompare = new List<FileToCompare>()
            {
                new FileToCompare(
                    new FileToCompareFactoryParam
                    {
                        ExternalId = externalId,
                        DecodedFile = jsonFileLeft,
                        Side = FileSide.LEFT
                    }),
            };


            var repository = new Mock<IComparisonRepository>();
            var fileRepository = new Mock<IFileRepository>();

            fileRepository.Setup(a => a.FindAsync(It.IsAny<Expression<Func<FileToCompare, bool>>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(filesToCompare);

            var factory = new ComparisonFactory(repository.Object, fileRepository.Object);

            //Act

            var comparisonResult = await factory.CreateAsync(externalId, cancellationToken);

            //Assert

            comparisonResult.Succeeded.Should().BeFalse();
            comparisonResult.Errors.Count().Should().BeGreaterThan(0);
            comparisonResult.Value.Should().BeNull();
        }
    }
}