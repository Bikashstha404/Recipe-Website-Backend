using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemDomain.Entities;

namespace RecipeManagementSystemAPI.Mappers
{
    public interface IRecipeMapper
    {
        Recipe AddRecipe(RecipeModel recipeModel);
    }
}
