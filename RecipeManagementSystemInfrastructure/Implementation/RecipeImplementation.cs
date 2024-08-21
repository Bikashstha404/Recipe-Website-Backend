using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemDomain.Entities;
using RecipeManagementSystemDomain.Enums.SubCategory;
using RecipeManagementSystemDomain.Enums;
using RecipeManagementSystemInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystemApplication.Response;

namespace RecipeManagementSystemInfrastructure.Implementation
{
    public class RecipeImplementation : IRecipe
    {
        private readonly RecipeDbContext _dbContext;

        public RecipeImplementation(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RecipeResponse> AddRecipe(Recipe recipe)
        {
            try
            {
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();

                return new RecipeResponse
                {
                    Success = true,
                    Message = "Recipe Added Successfully"
                };
            }
            catch(Exception ex)
            {
                return new RecipeResponse
                {
                    Success = false,
                    Message = ex.Message,
                };
            }


        }

        public async Task<RecipeResponse> EditRecipe(RecipeEditModel recipeEditModel)
        {
            Recipe recipeData = await _dbContext.Recipes.FindAsync(recipeEditModel.Id);
            if(recipeData == null)
            {
                return new RecipeResponse
                {
                    Success = false,
                    Message = "Recipe with this id doesn't exists."
                };
            }

            recipeData.Title = recipeEditModel.Title;
            recipeData.Descripton = recipeEditModel.Descripton;
            recipeData.PrepTime = recipeEditModel.PrepTime;
            recipeData.Calories = recipeEditModel.Calories;
            recipeData.MainCategory = recipeEditModel.MainCategory;
            recipeData.SubCategory = recipeEditModel.SubCategory;
            recipeData.ImageUrl = recipeEditModel.ImageUrl;
            recipeData.Ingredients = recipeEditModel.Ingredients;
            recipeData.Preparation = recipeEditModel.Preparation;

            await _dbContext.SaveChangesAsync();
            return new RecipeResponse
            {
                Success = true,
                Message = "Recipe Updated Successfully."
            };
        }

        public async Task<RecipeResponse> DeleteRecipe(Guid id)
        {
            var recipeData = await _dbContext.Recipes
                .Include(r => r.Ingredients)
                .SingleOrDefaultAsync(r => r.Id == id);
            if (recipeData == null)
            {
                return new RecipeResponse
                {
                    Success = false,
                    Message = "Recipe with this id doesn't exists."
                };
            }

            _dbContext.Recipes.Remove(recipeData);
            await _dbContext.SaveChangesAsync();
            return new RecipeResponse
            {
                Success = true,
                Message = "Recipe Deleted Successfully."
            };
        }

    }
}
