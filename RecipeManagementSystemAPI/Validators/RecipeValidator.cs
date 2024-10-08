﻿using RecipeManagementSystemDomain.Entities;
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
                case MainCategory.Appetizers:
                    if (!Enum.IsDefined(typeof(Appetizers), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for Appetizers.");
                    break;

                case MainCategory.MainDishes:
                    if (!Enum.IsDefined(typeof(MainDishes), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for MainDishes.");
                    break;

                case MainCategory.Desserts:
                    if (!Enum.IsDefined(typeof(Desserts), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for Desserts.");
                    break;

                case MainCategory.Drinks:
                    if (!Enum.IsDefined(typeof(Drinks), recipe.SubCategory))
                        throw new ArgumentException("Invalid SubCategory for Drinks.");
                    break;

                default:
                    throw new ArgumentException("Invalid MainCategory.");
            }
        }
    }
}
