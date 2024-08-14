using Microsoft.AspNetCore.Identity;
using RecipeManagementSystemAPI.Extensions;
using RecipeManagementSystemInfrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();
await app.SeedRolesAsync();

app.Run();
