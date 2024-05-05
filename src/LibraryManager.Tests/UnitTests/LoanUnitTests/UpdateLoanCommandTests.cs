using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.UpdateLoan;

namespace LibraryManager.Tests.UnitTests.LoanUnitTests
{
    public class UpdateLoanCommandHandlerTests
    {
        [Fact]
        public async Task Handle_LoanNotFound_ShouldReturnFailureResult()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var handler = new UpdateLoanCommandHandler(mockUnitOfWork.Object);
            var command = new UpdateLoanCommand { Id = Guid.NewGuid() }; // ID de empréstimo não existente

            mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(command.Id)).ReturnsAsync((Loan)null);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Empréstimo não encontrado.");
        }

        [Fact]
        public async Task Handle_SuccessfulUpdate_ShouldReturnSuccessResult()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var bookId = Guid.NewGuid();

            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();

            var book = new Book("Sample Book", "Sample Author", "1234567890", 2024);
            var loan = new Loan(Guid.NewGuid(), bookId);

            mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(loan);
            mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync(book);

            var handler = new UpdateLoanCommandHandler(mockUnitOfWork.Object);
            var command = new UpdateLoanCommand { Id = loanId };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
        }
    }
}
