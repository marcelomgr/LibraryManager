using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.UpdateUser;
using LibraryManager.Core.Integrations.ApiCepIntegration.Models;

namespace LibraryManager.Tests.UnitTests.UserUnitTests
{
    public class UpdateUserCommandTests
    {
        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFailureResultWithValidationErrors()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new UpdateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var command = new UpdateUserCommand();

            var validationErrors = new FluentValidation.Results.ValidationResult();
            validationErrors.Errors.Add(new FluentValidation.Results.ValidationFailure("Id", "Id do usuário é obrigatório."));
            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(validationErrors);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Id do usuário é obrigatório.");
        }

        [Fact]
        public async Task Handle_UserNotFound_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new UpdateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid(), // Id de usuário não existente
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(command.Id)).ReturnsAsync((User)null);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Usuário não encontrado.");
        }

        [Fact]
        public async Task Handle_DuplicateCPF_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUnitOfWork = MockObjects.GetMockUnitOfWork();
            var mockValidator = MockObjects.GetMockValidator<UpdateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new UpdateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var currentUserDb = new User { CPF = "12345678901" }; // Usuário corrente
            var existingUser = new User { CPF = "12345678910" }; // Usuário existente com o mesmo CPF

            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                Name = "teste",
                CPF = "12345678910",
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(command.Id)).ReturnsAsync(currentUserDb);
            mockUnitOfWork.Setup(uow => uow.Users.GetByCpfAsync(command.CPF)).ReturnsAsync(existingUser);

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
            var mockValidator = MockObjects.GetMockValidator<UpdateUserCommand>();
            var mockApiCepService = MockObjects.GetMockApiCepService();

            var handler = new UpdateUserCommandHandler(mockUnitOfWork.Object, mockValidator.Object, mockApiCepService.Object);

            var existingUser = new User { CPF = "12345678901" }; // Usuário existente
            var command = new UpdateUserCommand
            {
                Id = Guid.NewGuid(), // Id de usuário existente
                CPF = "12345678901", // CPF válido
                CEP = "00000000", // CEP inválido
            };

            mockValidator.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(command.Id)).ReturnsAsync(existingUser);

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
