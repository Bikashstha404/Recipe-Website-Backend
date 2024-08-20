using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemApplication.Response;
using RecipeManagementSystemDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Interface
{
    public interface IRecipe
    {
        Task<RecipeResponse> AddRecipe(Recipe recipe);
        Task<RecipeResponse> EditRecipe();
        Task<RecipeResponse> UpdateRecipe();
        Task<RecipeResponse> DeleteRecipe();
    }
}
