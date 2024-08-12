using Microsoft.EntityFrameworkCore;

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
        }
    }
}
