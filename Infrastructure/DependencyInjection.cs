using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions.Data;
using Quartz;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Domain.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        var connectionString = $"Data Source={dbHost};Database={dbName};User Id=sa;Password={dbPassword};Persist Security Info=True;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Integrated Security=false;";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
