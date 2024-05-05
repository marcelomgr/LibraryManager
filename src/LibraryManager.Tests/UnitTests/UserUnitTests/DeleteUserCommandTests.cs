using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.DeleteUser;

namespace LibraryManager.Tests.UnitTests.UserUnitTests
{
    public class DeleteUserCommandTests
    {
        [Fact]
        public async Task Handle_UserNotFound_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var handler = new DeleteUserCommandHandler(mockUnitOfWork.Object);

            var command = new DeleteUserCommand(Guid.NewGuid());

            mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(command.Id)).ReturnsAsync((User)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Usuário não encontrado.");
        }

        [Fact]
        public async Task Handle_UserDeletedSuccessfully_ShouldReturnSuccessResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var handler = new DeleteUserCommandHandler(mockUnitOfWork.Object);

            var userId = Guid.NewGuid(); // Gerar um novo Id de usuário
            
            var user = new User(); // Criar um novo usuário

            var command = new DeleteUserCommand(Guid.NewGuid());

            mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(command.Id)).ReturnsAsync(user);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().BeEmpty();
        }
    }
}
