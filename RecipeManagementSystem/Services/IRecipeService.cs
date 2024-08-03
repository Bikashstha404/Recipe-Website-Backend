using RecipeManagementSystem.Models;

namespace RecipeManagementSystem.Services
{
    public interface IRecipeService
    {
        Task<List<RecipeModel>> getRecipeData();
    }
}
