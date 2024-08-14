using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemInfrastructure.Data;
using RecipeManagementSystemInfrastructure.Implementation;

namespace RecipeManagementSystemAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Jwt Authentication

        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Swagger Options and other services like dependency injection

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<RecipeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RecipeConnectionString")));

            services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RecipeDbContext>();

            services.AddScoped<IAuth, AuthImplementation>();
        }
    }
}
