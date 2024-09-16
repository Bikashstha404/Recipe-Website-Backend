using RecipeManagementSystemApplication.Models.RecipeModels;
using RecipeManagementSystemDomain.EntitesTypesModel;
using RecipeManagementSystemDomain.Entities;
using RecipeManagementSystemDomain.Enums;

namespace RecipeManagementSystemAPI.Mappers
{
    public class RecipeMapper : IRecipeMapper
    {
        public Recipe AddRecipe(RecipeAddModel recipeModel)
        {
            Recipe recipe = new Recipe
            {
                Title = recipeModel.Title,
                Description = recipeModel.Description,
                PrepTime = recipeModel.PrepTime,
                Calories = recipeModel.Calories,
                MainCategory = recipeModel.MainCategory,
                SubCategory = recipeModel.SubCategory,
                ImageUrl = recipeModel.ImageUrl,
                Ingredients = recipeModel.Ingredients,
                Preparation = recipeModel.Preparation,
            };

            return recipe;
        }
    }
}
