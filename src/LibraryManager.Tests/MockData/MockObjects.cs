using Moq;
using FluentValidation;
using LibraryManager.Core.Repositories;
using LibraryManager.Core.Services.AuthService;
using LibraryManager.Core.Integrations.ApiCepIntegration;

namespace LibraryManager.Tests.MockData
{
    public static class MockObjects
    {
        public static Mock<IUnitOfWork> GetMockUnitOfWork()
        {
            return new Mock<IUnitOfWork>();
        }

        public static Mock<IApiCepService> GetMockApiCepService()
        {
            return new Mock<IApiCepService>();
        }

        public static Mock<IAuthService> GetMockAuthService()
        {
            return new Mock<IAuthService>();
        }

        public static Mock<IValidator<T>> GetMockValidator<T>() where T : class
        {
            return new Mock<IValidator<T>>();
        }

        public static Mock<IUserRepository> GetMockUserRepository()
        {
            return new Mock<IUserRepository>();
        }
    }
}
