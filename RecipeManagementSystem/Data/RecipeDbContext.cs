using Microsoft.EntityFrameworkCore;
using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) :base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("RecipeConnectionString");
        //}
        public DbSet<RecipeModel> Recipes { get; set; }
        public DbSet<MenuModel> Menus { get; set; }
    }
}
    