using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class AccountService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly EmailService _emailService;

        public AccountService(ICustomerRepository customerRepo, EmailService emailService)
        {
            _customerRepo = customerRepo;
            _emailService = emailService;
        }

        public async Task<string> SendVerificationCodeAsync(string email)
        {
            var user = await _customerRepo.GetUserByEmail(email);
            if (user == null) return "البريد غير موجود";

            var code = new Random().Next(100000, 999999).ToString();
            user.VerificationCode = code;
            user.CodeExpiry = DateTime.UtcNow.AddMinutes(10);

            await _customerRepo.Update(user);
            await _emailService.SendEmailAsync(email, "رمز التحقق", $"رمزك هو: {code}");

            return "تم إرسال رمز التحقق إلى البريد الإلكتروني";
        }

        public async Task<bool> VerifyCodeAsync(string email, string code)
        {
            var user = await _customerRepo.GetUserByEmail(email);
            if (user == null || user.VerificationCode != code || user.CodeExpiry < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task<string> ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _customerRepo.GetUserByEmail(email);
            if (user == null || user.VerificationCode != code || user.CodeExpiry < DateTime.UtcNow)
                return "رمز التحقق غير صالح أو منتهي";

            user.Password = newPassword; // استخدمي تشفير حقيقي
            user.VerificationCode = null;
            user.CodeExpiry = null;

            await _customerRepo.Update(user);
            return "تم تغيير كلمة المرور بنجاح";
        }
    }
}
