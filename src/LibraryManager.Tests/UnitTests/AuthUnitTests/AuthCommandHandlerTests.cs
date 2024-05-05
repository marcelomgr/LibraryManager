using Moq;
using FluentAssertions;
using LibraryManager.Core.Entities;
using LibraryManager.Tests.MockData;
using LibraryManager.Application.Commands.AuthUser;

namespace LibraryManager.Tests.UnitTests.AuthUnitTests
{
    public class AuthUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_EmptyCPFAndPassword_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUserRepository = MockObjects.GetMockUserRepository();
            var mockAuthService = MockObjects.GetMockAuthService();

            var handler = new AuthUserCommandHandler(mockUserRepository.Object, mockAuthService.Object);

            var command = new AuthUserCommand
            {
                CPF = "", // CPF vazio
                Password = "" // Senha vazia
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Informe usuário e senha.");
        }

        [Fact]
        public async Task Handle_InvalidUserCredentials_ShouldReturnFailureResultWithErrorMessage()
        {
            // Arrange
            var mockUserRepository = MockObjects.GetMockUserRepository();
            var mockAuthService = MockObjects.GetMockAuthService();

            var handler = new AuthUserCommandHandler(mockUserRepository.Object, mockAuthService.Object);

            var command = new AuthUserCommand
            {
                CPF = "12345678901", // CPF válido para teste
                Password = "password" // Senha inválida
            };

            mockUserRepository.Setup(repo => repo.ValidateUserCredentialsAsync(command.CPF, It.IsAny<string>()))
                              .ReturnsAsync((User)null); // Simulando que o usuário não foi encontrado

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Usuário ou senha inválidos.");
        }

        [Fact]
        public async Task Handle_ValidUserCredentials_ShouldReturnSuccessResultWithAuthViewModel()
        {
            // Arrange
            var mockUserRepository = MockObjects.GetMockUserRepository();
            var mockAuthService = MockObjects.GetMockAuthService();

            var handler = new AuthUserCommandHandler(mockUserRepository.Object, mockAuthService.Object);

            var user = new User
            {
                Name = "John Doe",
                CPF = "12345678901", // CPF válido para teste
                Password = "hashed_password" // Senha válida
            };

            var command = new AuthUserCommand
            {
                CPF = user.CPF,
                Password = "password" // Senha válida
            };

            mockUserRepository.Setup(repo => repo.ValidateUserCredentialsAsync(command.CPF, It.IsAny<string>()))
                              .ReturnsAsync(user); // Simulando que o usuário foi encontrado

            var jwtToken = "mock_jwt_token";
            mockAuthService.Setup(service => service.GenerateJwtToken(user))
                           .Returns(jwtToken); // Simulando a geração de um token JWT

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Name.Should().Be(user.Name);
            result.Data.CPF.Should().Be(user.CPF);
            result.Data.Token.Should().Be(jwtToken);
        }
    }
}
