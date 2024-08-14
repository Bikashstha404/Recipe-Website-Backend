using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemApplication.Response;

namespace RecipeManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _iAuth;

        public AuthController(IAuth iAuth)
        {
            _iAuth = iAuth;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            SignUpResponse response = await _iAuth.SignUp(signUpModel);
            if (response.Success)
            {
                return Ok(new {Message = response.Message});
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
