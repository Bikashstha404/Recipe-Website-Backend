using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace RecipeManagementSystemAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            // Get the hosting environment
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); // This should be before UseEndpoints

            app.UseCors("AllowOrigin");
            app.UseAuthentication();

            app.UseAuthorization();

            // UseEndpoints to map controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Cook", "FoodEnthusiast", "Planner"};
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
    }
}
