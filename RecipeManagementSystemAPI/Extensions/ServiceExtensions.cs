using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecipeManagementSystemAPI.Mappers;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemInfrastructure.Data;
using RecipeManagementSystemInfrastructure.Implementation;
using RecipeManagementSystemInfrastructure.Implementation.AuthImplementation;
using System.Text;

namespace RecipeManagementSystemAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Jwt Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                    .UTF8.GetBytes(configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Swagger Options and other services like dependency injection

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Recipe Management API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please Enter a Token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                             Reference = new OpenApiReference
                             {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                             }
                        },
                        new string[]{}
                    }
                });
            });


            services.AddDbContext<RecipeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RecipeConnectionString")));

            services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RecipeDbContext>();

            services.AddScoped<IAuth, AuthImplementation>();
            services.AddScoped<IEmailService, EmailServicemImplementation>();
            services.AddScoped<IRecipe, RecipeImplementation>();
            services.AddScoped<IRecipeMapper, RecipeMapper>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
        }
    }
}
