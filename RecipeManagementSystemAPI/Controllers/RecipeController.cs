using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeManagementSystemAPI.Mappers;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemDomain.Entities;
using RecipeManagementSystemDomain.Enums.SubCategory;
using RecipeManagementSystemDomain.Enums;
using RecipeManagementSystemAPI.Validators;
using RecipeManagementSystemInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using RecipeManagementSystemApplication.Models.RecipeModels;

namespace RecipeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipe _iRecipe;
        private readonly IRecipeMapper _iRecipeMapper;
        private readonly RecipeDbContext dbContext;

        public RecipeController(IRecipe iRecipe, IRecipeMapper iRecipeMapper, RecipeDbContext dbContext)
        {
            _iRecipe = iRecipe;
            _iRecipeMapper = iRecipeMapper;
            this.dbContext = dbContext;
        }

        [HttpGet("GetAllRecipes")]
        public IActionResult GetAllRecipes()
        {
            var recipes = dbContext.Recipes
                .Include(r => r.Ingredients)
                .ToList();

            if (recipes == null || recipes.Count == 0)
            {
                return NotFound(new { Message = "No recipes found." });
            }

            return Ok(recipes);
        }

        [HttpPost("AddRecipe")]
        public async Task<IActionResult> AddRecipe(RecipeAddModel recipeModel)
        {
            Recipe recipe = _iRecipeMapper.AddRecipe(recipeModel);
            if(recipe != null)
            {
                try
                {
                    RecipeValidator.ValidateandSetSubCategory(recipe);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }

                var response = await _iRecipe.AddRecipe(recipe);
                if (response.Success)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            else
            {
                return BadRequest("Error occured during adding recipe model.");
            }

        }

        [HttpPost("EditRecipe")]
        public async Task<IActionResult> EditRecipe(RecipeEditModel recipeEditModel)
        {
            var response = await _iRecipe.EditRecipe(recipeEditModel);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("DeleteRecipe/{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var response = await _iRecipe.DeleteRecipe(id);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
