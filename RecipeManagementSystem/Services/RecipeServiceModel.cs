using Microsoft.EntityFrameworkCore;
using RecipeManagementSystem.Data;
using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Services
{
    public class RecipeServiceModel : IRecipeService    
    {
        private readonly RecipeDbContext _dbContext;

        public RecipeServiceModel(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<RecipeModel>> getRecipeData()
        {
            var recipeData = new List<RecipeModel>();
            try
            {
                //recipeData = await _dbContext.Recipes.ToListAsync();
                //recipeData = (await _dbContext.Recipes.ToListAsync()).OrderByDescending(x => x.Id).ToList();
                recipeData = await _dbContext.Recipes.OrderByDescending(x => x.Id).ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return recipeData;
        }
    }
}
