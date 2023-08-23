using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication;
using Bookify.Infrastructure.Clock;
using Bookify.Infrastructure.Data;
using Bookify.Infrastructure.Email;
using Bookify.Infrastructure.Outbox;
using Bookify.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace Bookify.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            var connectionString =
                configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(
                opt =>
                {
                    opt.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
                });
            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(connectionString));
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            AddBackgroundJobs(services, configuration);

            // Temp
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUserContext, UserContext>();
            // Temp

            return services;
        }

        private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
        }
    }
}