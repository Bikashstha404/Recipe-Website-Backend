using RecipeManagementSystemApplication.Models.AuthModels;
using RecipeManagementSystemApplication.Response.SignUpAndLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Interface
{
    public interface IAuth
    {
        Task<SignUpResponse> SignUp(SignUpModel signUpModel);
        Task<LoginResponse> Login(LoginModel loginModel);
        Task<RefreshTokenResponse> RefreshToken(string accessToken, string refreshToken);
        Task<EmailModelResponse> EmailModel(string email);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}
