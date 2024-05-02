using DoAn.Data;
using DoAn.Helper;
using DoAn.Models;
using DoAn.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DoAn.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext context;
        private readonly ILogger<AccountRepository> logger;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            ILogger<AccountRepository> logger

           )
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this._configuration = configuration;
            this.roleManager = roleManager;
            this.context = context;
            this.logger = logger;

        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private string GenerateAccessToken(IdentityUser user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task StoreRefreshTokenAsync(ApplicationUser user, RefreshToken refreshToken)
        {
            var existingToken = await context.RefreshTokens
        .FirstOrDefaultAsync(rt => rt.UserId == user.Id); // find token by user id 

            if (existingToken != null)  // nếu token tồn tại thì xóa 
            {
                context.RefreshTokens.Remove(existingToken);
            }
            // Thêm refresh token mới vào database
            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
        }
        public async Task<TokenModel> SignInAsync(SignInModel signInModel)
        {
            logger.LogInformation("Sign in user with email: {Email}", signInModel.Email);
            var user = await userManager.FindByEmailAsync(signInModel.Email);
            if (user == null || !(await userManager.CheckPasswordAsync(user, signInModel.Password)))
            {
                throw new AuthenticationException("Email or password is incorrect");
            }

            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, false);
            if (!result.Succeeded)
            {
                throw new AuthenticationException("Failed to sign in.");
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                Token = GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.AddYears(1),
            };
            await StoreRefreshTokenAsync(user, refreshToken);
            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
        public async Task<IdentityResult> SignUpAsync(RegisterModel registerModel)
        {
            var user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };
            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                await userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }
        public async Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel)
        {
            var storedToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == tokenModel.RefreshToken);
            if (storedToken == null)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }
            var user = await userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                throw new SecurityTokenException("User not found.");
            }
            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken();

            storedToken.Token = newRefreshToken; //cập nhật refresh token mới
            storedToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);
            context.RefreshTokens.Update(storedToken);
            await context.SaveChangesAsync();
            return new TokenModel
            {
                AccessToken = newAccessToken,
                RefreshToken = storedToken.Token
            };
        }

        public async Task<ApiResponse> LogoutAsync(RequestRefreshToken requestRefreshToken)
        {
            var refreshToken = await context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == requestRefreshToken.RefreshToken);
            if (refreshToken == null)
            {
                return new ApiResponse(404, "Refresh token not found.");
            }
            if (refreshToken.IsRevoked)
            {
                return new ApiResponse(400, "Refresh token already revoked.");
            }

            refreshToken.IsRevoked = true;
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Logout successfully");
        }
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await userManager.FindByEmailAsync(changePasswordModel.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User not found");
            }
            var result = await userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }
            return new OkObjectResult("Change password successfully");
        }


    }
}
