using Moq;
using FluentAssertions;
using LibraryManager.Tests.MockData;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Commands.CreateBook;

namespace LibraryManager.Tests.UnitTests.BookUnitTests
{
    public class CreateBookCommandTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnSuccessResultWithBookId()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateBookCommand>();

            var handler = new CreateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new CreateBookCommand
            {
                Title = "Book Title",
                Author = "Book Author",
                ISBN = "1234567890",
                PublicationYear = 2022
            };

            mockUnitOfWork.Setup(uow => uow.Books).Returns(new Mock<IBookRepository>().Object);
            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().BeEmpty();
            result.Data.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailureResultWithValidationErrors()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateBookCommand>();

            var handler = new CreateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new CreateBookCommand();

            var validationErrors = new FluentValidation.Results.ValidationResult();
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("Title", "Título é obrigatório."));
            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(validationErrors);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Título é obrigatório.");
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateBookCommand>();

            var handler = new CreateBookCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new CreateBookCommand();

            var validationErrors = new FluentValidation.Results.ValidationResult();
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("ISBN", "ISBN é obrigatório."));
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("PublicationYear", "Ano de publicação é obrigatório."));
            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(validationErrors);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("ISBN é obrigatório.");
            result.Message.Should().Contain("Ano de publicação é obrigatório.");
        }
    }

}
