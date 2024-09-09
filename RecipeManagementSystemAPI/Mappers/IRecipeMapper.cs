using RecipeManagementSystemApplication.Models.RecipeModels;
using RecipeManagementSystemDomain.Entities;

namespace RecipeManagementSystemAPI.Mappers
{
    public interface IRecipeMapper
    {
        Recipe AddRecipe(RecipeAddModel recipeModel);
    }
}
