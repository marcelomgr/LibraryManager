using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Commands.CreateLoan;

namespace LibraryManager.Tests.UnitTests.LoanUnitTests
{
    public class CreateLoanCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateLoanSuccessfully()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateLoanCommand>();
            var handler = new CreateLoanCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new CreateLoanCommand
            {
                UserId = Guid.NewGuid(),
                BookId = Guid.NewGuid()
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var book = new Book(); // Assume book exists
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.BookId)).ReturnsAsync(book);
            mockUnitOfWork.Setup(uow => uow.Loans).Returns(new Mock<ILoanRepository>().Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Handle_BookAlreadyLent_ShouldReturnFailure()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateLoanCommand>();
            var handler = new CreateLoanCommandHandler(mockUnitOfWork.Object, mockValidator.Object);

            var command = new CreateLoanCommand
            {
                UserId = Guid.NewGuid(),
                BookId = Guid.NewGuid()
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var book = new Book("Book Title", "Book Author", "1234567890", 2024);
            book.Lent(); // Livro já emprestado
            
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(command.BookId)).ReturnsAsync(book);
            mockUnitOfWork.Setup(uow => uow.Loans).Returns(new Mock<ILoanRepository>().Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("O livro está emprestado.");
        }
    }
}
