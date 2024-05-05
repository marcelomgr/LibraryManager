using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Core.Integrations.ApiCepIntegration.Models;

namespace LibraryManager.Tests.UnitTests.UserUnitTests
{
    public class CreateUserCommandTests
    {
        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailureResultWithValidationErrors()
        {
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new CreateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var command = new CreateUserCommand();

            var validationErrors = new FluentValidation.Results.ValidationResult();
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("CPF", "CPF é obrigatório."));
            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(validationErrors);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("CPF é obrigatório.");
        }

        [Fact]
        public async Task Handle_DuplicateCPF_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new CreateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var command = new CreateUserCommand
            {
                CPF = "12345678901", // CPF válido para teste
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockUnitOfWork.Setup(uow => uow.Users.GetByCpfAsync(command.CPF)).ReturnsAsync(new User());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("CPF já cadastrado.");
        }

        [Fact]
        public async Task Handle_InvalidCep_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<CreateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new CreateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var command = new CreateUserCommand
            {
                CPF = "12345678901", // CPF válido para teste
                CEP = "00000000", // CEP inválido
                Password = "teste",
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockUnitOfWork.Setup(uow => uow.Users.GetByCpfAsync(command.CPF)).ReturnsAsync((User)null);

            // Simular o serviço de CEP não retornando informações
            mockApiCepService.Setup(service => service.GetByCep(command.CEP))
                .ReturnsAsync((CepViewModel)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("CEP não encontrado.");
        }
    }
}
