using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeManagementSystemAPI.Dtos;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var response = await _iAuth.Login(loginModel);
            if (response.Success)
            {
                return Ok(new TokenApiDto()
                {
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken,
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenApiDto tokenApiDto)
        {
            var accessToken = tokenApiDto.AccessToken;
            var refreshToken = tokenApiDto.RefreshToken;

            var response = await _iAuth.RefreshToken(accessToken, refreshToken);
            if (response.Success)
            {
                return Ok(new TokenApiDto()
                {
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken,
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        } 
    }
}
