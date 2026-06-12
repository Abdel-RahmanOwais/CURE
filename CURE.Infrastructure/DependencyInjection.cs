using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Repositories.IBooking;
using CURE.Application.Interfaces.Security;
using CURE.Infrastructure.Data;
using CURE.Infrastructure.Repositories;
using CURE.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CURE.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            #region User Scope
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Patient Scope 
            services.AddScoped<IPatientRepository, PatientRepository>();
            #endregion

            services.AddScoped<INurseRepository, NurseRepository>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IBookingStatusHistoryRepository, BookingStatusHistoryRepository>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IPasswordHash, PasswordHasher>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}


