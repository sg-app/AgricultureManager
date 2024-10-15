using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AgricultureManager.Core.Application.Tests.Common
{
    public class GetByIdCommandHandlerTests
    {
        private Mock<IAppDbContext> _dbContextMock;
        private Mock<IMapper> _mapperMock;
        private GetByIdCommandHandler<CultureVm> _handler;

        [SetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<IAppDbContext>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetByIdCommandHandler<CultureVm>(_dbContextMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnViewModel_WhenEntityIsFound()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = new Culture { Id = entityId, Name = "Test Culture" };
            var viewModel = new CultureVm { Id = entityId, Name = "Test Culture" };

            _dbContextMock.Setup(db => db.FindAsync(typeof(Culture), new object[] { entityId }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CultureVm>(entity)).Returns(viewModel);

            var request = new GetByIdCommand<CultureVm> { Id = entityId };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(viewModel);
        }
    }
}
