using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestCommand dto)
        {
            var result = await _accountService.SendVerificationCodeAsync(dto.Email);
            return Ok(result);
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeCommand dto)
        {
            var isValid = await _accountService.VerifyCodeAsync(dto.Email, dto.Code);
            return isValid ? Ok("رمز التحقق صحيح") : BadRequest("رمز التحقق غير صحيح أو منتهي");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand dto)
        {
            var result = await _accountService.ResetPasswordAsync(dto.Email, dto.Code, dto.NewPassword);
            return Ok(result);
        }
    }
}
