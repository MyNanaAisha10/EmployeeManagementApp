//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//namespace Application.Services.Account
//{
//    public class DummyEmailSender : IEmailSender<IdentityUser>
//    {
//        public Task SendConfirmationLinkAsync(IdentityUser user, string email, string confirmationLink)
//        {
//            Console.WriteLine($"Confirmation Link: {confirmationLink}");
//            return Task.CompletedTask;
//        }

//        public Task SendPasswordResetCodeAsync(IdentityUser user, string email, string resetCode)
//        {
//            Console.WriteLine($"Password Reset Code: {resetCode}");
//            return Task.CompletedTask;
//        }

//        public Task SendPasswordResetLinkAsync(IdentityUser user, string email, string resetLink)
//        {
//            Console.WriteLine($"Reset Link: {resetLink}");
//            return Task.CompletedTask;
//        }
//    }

//}
