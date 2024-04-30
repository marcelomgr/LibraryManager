using MediatR;
using FluentValidation;
using LibraryManager.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Application.Commands.AuthUser;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Application.Commands.UpdateUser;

namespace LibraryManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddMediator()
                .AddValidators();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationModule));

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SignUpUserCommand>, SignUpUserValidator>();
            services.AddTransient<IValidator<UpdateUserCommand>, UpdateUserValidator>();

            return services;
        }
    }
}
