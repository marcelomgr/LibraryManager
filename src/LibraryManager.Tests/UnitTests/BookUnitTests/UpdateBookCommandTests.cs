using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.UpdateBook;

namespace LibraryManager.Tests.UnitTests.BookUnitTests
{
    public class UpdateBookCommandTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateBookAndReturnSuccessResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateBookCommand>();

            var handler = new UpdateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new UpdateBookCommand
            {
                Id = Guid.NewGuid(),
                Title = "New Title",
                Author = "New Author",
                ISBN = "New ISBN",
                PublicationYear = 2022
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            var existingBook = new Book("Old Title", "Old Author", "Old ISBN", 2000);
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.Id)).ReturnsAsync(existingBook);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            existingBook.Title.Should().Be("New Title");
            existingBook.Author.Should().Be("New Author");
            existingBook.ISBN.Should().Be("New ISBN");
            existingBook.PublicationYear.Should().Be(2022);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailureResultWithValidationErrors()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateBookCommand>();

            var handler = new UpdateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new UpdateBookCommand();

            var validationErrors = new FluentValidation.Results.ValidationResult();
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("Id", "ID é obrigatório."));

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(validationErrors);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("ID é obrigatório.");
        }

        [Fact]
        public async Task Handle_BookNotFound_ShouldReturnFailureResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateBookCommand>();

            var handler = new UpdateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new UpdateBookCommand
            {
                Id = Guid.NewGuid(), // ID de um livro que não existe
                Title = "New Title",
                Author = "New Author",
                ISBN = "New ISBN",
                PublicationYear = 2022
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.Id)).ReturnsAsync((Book)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Livro não encontrado.");
        }
    }
}