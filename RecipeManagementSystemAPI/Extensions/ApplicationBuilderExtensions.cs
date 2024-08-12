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

            app.UseAuthorization();

            // UseEndpoints to map controllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            //Set user roles in here
        }
    }
}
