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

        public async Task<AddRecipeResponse> AddRecipe(Recipe recipe)
        {
            try
            {
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();

                return new AddRecipeResponse
                {
                    Success = true,
                    Message = "Recipe Added Successfully"
                };
            }
            catch(Exception ex)
            {
                return new AddRecipeResponse
                {
                    Success = false,
                    Message = ex.Message,
                };
            }


        }
    }
}
