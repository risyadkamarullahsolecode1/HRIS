using HRIS.Application.Interfaces;
using HRIS.Application.Services;
using HRIS.Domain.Entities;
using HRIS.Domain.Interfaces;
using HRIS.Infrastructure.Data;
using HRIS.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MiniProject7.Application.Services;
using MiniProject7.Infrastructure.Data.Repository;
using System.Text;

namespace HRIS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HRISContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options =>

            {

                options.Password.RequireLowercase = false;

                options.Password.RequireUppercase = false;

                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<HRISContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HRManagerOnly", policy => policy.RequireRole("HR Manager"));
                options.AddPolicy("SupervisorOnly", policy => policy.RequireRole("Supervisor"));
                options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Employee"));
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IWorksonRepository, WorksonRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IWorkflowRepository, WorkflowRepository>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IDashboardService, DasboardService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            return services;
        }
    }
}
