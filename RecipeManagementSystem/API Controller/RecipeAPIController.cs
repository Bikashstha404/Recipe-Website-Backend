using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using RecipeManagementSystem.Data;
using RecipeManagementSystem.Models;
using RecipeManagementSystem.Services;

namespace RecipeManagementSystem.API_Controller
{
    //[Authorize]
    [Route("recipeApi")]
    public class RecipeAPIController : ControllerBase
    {
        private readonly RecipeDbContext _recipeDbContext;
        private readonly IConfiguration _configuration;
        private readonly IRecipeService _recipeService;

        public RecipeAPIController(RecipeDbContext recipeDbContext, IConfiguration configuration, IRecipeService iRecipeService)
        {
            _recipeDbContext = recipeDbContext;
            _configuration = configuration;
            _recipeService = iRecipeService;
        }

        [Route("getRecipeData")]
        public async Task<IActionResult> getRecipeData()
        {
            try
            {
                var recipeData = await _recipeDbContext.Recipes.ToListAsync();
                return Ok(recipeData);
            }
            catch(Exception exp){

            }
            return Ok();
        }
        
        [Route("getRecipeAllData/{id}")]
        public async Task<IActionResult> getRecipeAllData(int id)
        {
            var dataList = new List<RecipeModel>();
            try
            {
                var connectionString = _configuration.GetConnectionString("RecipeConnectionString");
                var query = $@"select * from Recipes";
                //var firstData = await _recipeDbContext.Recipes.FirstOrDefaultAsync();

                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT Id, Name FROM Recipes WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync()) 
                        {
                            var Id = reader.GetInt32(0);
                            var name = reader.GetString(1);
                            dataList.Add(new RecipeModel
                            {
                                Id = Id,
                                Name = name
                            });
                        }
                    }
                    await connection.CloseAsync();
                }
            }
            catch (Exception exp)
            {

            }
            finally
            {
            }
            return Ok(dataList);

        }

        [Route("getRecipeDataFromService")]
        public async Task<IActionResult> getRecipeDataFromService()
        {
            var recipeData = new List<RecipeModel>();
            try
            {
                recipeData = await _recipeService.getRecipeData();
            }
            catch (Exception exp)
            {

            }
            return Ok(recipeData);
        }
    }
}
