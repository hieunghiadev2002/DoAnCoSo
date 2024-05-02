using DoAn.Data;
using DoAn.Helper;
using DoAn.Models;
using DoAn.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationDbContext _context; 
        public AccountsController(IAccountRepository accountRepository, ApplicationDbContext context)
        {
            _accountRepository = accountRepository;
            _context = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _accountRepository.SignInAsync(signInModel);
            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountRepository.SignUpAsync(registerModel);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Registration successfully.");
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _accountRepository.RefreshTokenAsync(tokenModel);
            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RequestRefreshToken requestRefreshToken)
        {
            ApiResponse response = await _accountRepository.LogoutAsync(requestRefreshToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountRepository.ChangePassword(changePasswordModel);
            return result;
        }
    }
}
