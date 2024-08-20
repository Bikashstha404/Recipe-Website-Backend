using RecipeManagementSystemDomain.Entities;
using RecipeManagementSystemDomain.Enums.SubCategory;
using RecipeManagementSystemDomain.Enums;

namespace RecipeManagementSystemAPI.Validators
{
    public static class RecipeValidator
    {
        public static void ValidateandSetSubCategory(Recipe recipe)
        {
            switch (recipe.MainCategory)
            {
                case MainCategory.EntreesBreakfast:
                    if (!Enum.IsDefined(typeof(EntreesBreakfast), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for EntreesBreakfast.");
                    break;

                case MainCategory.MainDishes:
                    if (!Enum.IsDefined(typeof(MainDishes), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for MainDishes.");
                    break;

                case MainCategory.Appetizers:
                    if (!Enum.IsDefined(typeof(Appetizers), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for Appetizers.");
                    break;

                default:
                    throw new ArgumentException("Invalid MainCategory.");
            }
        }
    }
}
