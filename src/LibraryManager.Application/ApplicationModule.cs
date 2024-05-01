using MediatR;
using FluentValidation;
using LibraryManager.Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using LibraryManager.Application.Commands.SignUpUser;
using LibraryManager.Application.Commands.UpdateUser;
using LibraryManager.Application.Commands.CreateBook;
using LibraryManager.Application.Commands.UpdateBook;
using LibraryManager.Application.Commands.CreateLoan;

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
            services.AddTransient<IValidator<CreateUserCommand>, CreateUserValidator>();
            services.AddTransient<IValidator<UpdateUserCommand>, UpdateUserValidator>();
            services.AddTransient<IValidator<CreateBookCommand>, CreateBookValidator>();
            services.AddTransient<IValidator<UpdateBookCommand>, UpdateBookValidator>();
            services.AddTransient<IValidator<CreateLoanCommand>, CreateLoanValidator>();

            return services;
        }
    }
}
