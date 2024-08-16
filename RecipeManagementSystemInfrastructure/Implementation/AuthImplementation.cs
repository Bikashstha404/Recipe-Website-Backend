using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemApplication.Models;
using RecipeManagementSystemApplication.Response;
using RecipeManagementSystemDomain.Enums;
using RecipeManagementSystemInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemInfrastructure.Implementation
{
    public class AuthImplementation : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthImplementation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<SignUpResponse> SignUp(SignUpModel signUpModel)
        {
            var userData = await _userManager.FindByEmailAsync(signUpModel.Email);
            var username = await _userManager.FindByNameAsync(signUpModel.Username);

            if(userData != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "User with this email address already exists."
                };
            }

            if (username != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Username already exists."
                };
            }

            if (signUpModel.Password != signUpModel.ConfirmPassword)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Both Password and ConfirmPassword should be same."
                };
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = signUpModel.Username,
                Email = signUpModel.Email,
                Role = signUpModel.Role,
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            if(result.Succeeded)
            {
                if(signUpModel.Email == _configuration["AdminEmail"])
                {
                    var adminExists = await _userManager.GetUsersInRoleAsync("Admin");
                    if (adminExists.Any())
                    {
                        return new SignUpResponse
                        {
                            Success = false,
                            Message = "An admin already exists."
                        };
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");

                        user.Role = null;
                        await _userManager.UpdateAsync(user);

                        return new SignUpResponse
                        {
                            Success = true,
                            Message = "Admin Registration Successfull."
                        };
                    }
                }
                
                if(signUpModel.Role == Roles.Cook){
                    await _userManager.AddToRoleAsync(user, "Cook");
                    return new SignUpResponse
                    {
                        Success = true,
                        Message = "Cook Registration Successfull."
                    };
                }
                
                if(signUpModel.Role == Roles.FoodEnthusiast)
                {
                    await _userManager.AddToRoleAsync(user, "FoodEnthusiast");
                    return new SignUpResponse
                    {
                        Success = true,
                        Message = "FoodEnthusiast Registration Successfull."
                    };
                }
                
                if (signUpModel.Role == Roles.Planner)
                {
                    await _userManager.AddToRoleAsync(user, "Planner");
                    return new SignUpResponse
                    {
                        Success = true,
                        Message = "Planner Registration Successfull."
                    };
                }

                return new SignUpResponse
                {
                    Success = false,
                    Message = "Invalid Role Specified."
                };
            }
            else
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                };
            }
        }

        public async Task<LoginResponse> Login(LoginModel loginModel)
        {
            var userData = await _userManager.FindByEmailAsync(loginModel.Email);
            if (userData == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "No user found with this email address."
                };
            }

            var user = await _signInManager.PasswordSignInAsync(userData.UserName, loginModel.Password, false, false);
            if (user.Succeeded)
            {
                Token token = new Token(_configuration, _userManager);
                var accessToken = await token.CreateAccessToken(userData);
                return new LoginResponse
                {
                    Success = true,
                    AccessToken = accessToken,
                    RefreshToken = null,
                    Message = "Login Successfull"
                };
            }
            else
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "Invalid Password"
                };
            }
        }
    }
}
