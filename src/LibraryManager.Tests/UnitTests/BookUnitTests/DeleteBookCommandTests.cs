using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.DeleteBook;

namespace LibraryManager.Tests.UnitTests.BookUnitTests
{
    public class DeleteBookCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteBookAndReturnSuccessResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();

            var handler = new DeleteBookCommandHandler(mockUnitOfWork.Object);

            var command = new DeleteBookCommand(Guid.NewGuid());

            var existingBook = new Book("Title", "Author", "ISBN", 2000);
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.Id)).ReturnsAsync(existingBook);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            // Verifica se o método DeleteAsync foi chamado
            mockUnitOfWork.Verify(uow => uow.Books.DeleteAsync(existingBook.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_BookNotFound_ShouldReturnFailureResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();

            var handler = new DeleteBookCommandHandler(mockUnitOfWork.Object);

            var command = new DeleteBookCommand(Guid.NewGuid());

            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.Id)).ReturnsAsync((Book)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Livro não encontrado.");

            // Verifica se o método DeleteAsync não foi chamado
            mockUnitOfWork.Verify(uow => uow.Books.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
