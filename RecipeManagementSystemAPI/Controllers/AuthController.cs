using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmailService _iEmailService;

        public AuthController(IAuth iAuth, IEmailService iEmailService)
        {
            _iAuth = iAuth;
            _iEmailService = iEmailService;
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

        [HttpPost("SendResetPasswordEmail/{email}")]
        public async Task<IActionResult> SendResetPasswordEmail(string email)
        {
            var response = await _iAuth.EmailModel(email);
            EmailModel emailModel;
            if (response.Success)
            {
                emailModel = response.EmailModel;
            }
            else
            {
                return BadRequest(response.Message);
            }

            _iEmailService.SendEmail(emailModel);
            return Ok(new 
            {
                StatusCode = 200,
                Message = "Email Sent!" 
            });

        }
        
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var response = await _iAuth.ResetPassword(resetPasswordModel);
            if (response.Success)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = response.Message
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
