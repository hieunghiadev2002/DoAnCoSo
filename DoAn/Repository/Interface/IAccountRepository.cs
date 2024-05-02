using DoAn.Helper;
using DoAn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoAn.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(RegisterModel registerModel);
        Task<TokenModel> SignInAsync(SignInModel signInModel); 
        Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel);
        Task<ApiResponse> LogoutAsync(RequestRefreshToken refreshToken);
        //Forgot password 
        Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel);
    }
}
