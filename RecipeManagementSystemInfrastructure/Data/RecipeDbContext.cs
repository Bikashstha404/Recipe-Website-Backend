using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemInfrastructure.Data
{
    public class RecipeDbContext : IdentityDbContext<ApplicationUser>
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {

        }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
