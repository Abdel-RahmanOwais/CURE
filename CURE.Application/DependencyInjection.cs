using CURE.Application.Interfaces.Services;
using CURE.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CURE.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<INurseService, NurseService>();

            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
