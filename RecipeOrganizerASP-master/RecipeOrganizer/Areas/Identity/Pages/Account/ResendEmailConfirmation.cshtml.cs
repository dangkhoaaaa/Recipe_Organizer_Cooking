// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Services.Models.Authentication;

namespace RecipeOrganizer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Confirm your email",
                $"<tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family: Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;'><h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Welcome to Recipe Organizer App!</h1></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>You have been registered in the Recipe Organizer app. Please confirm your account by clicking the button below:</td></tr><tr><td align='left' bgcolor='#ffffff'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center' bgcolor='#ffffff' style='padding: 12px;'><table border='0' cellpadding='0' cellspacing='0'><tr><td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'><a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary' style='padding: 16px 36px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px; background-color: #ff472f; border-color: #ffffff; font-weight: bold;'>Click here</a></td></tr></table></td></tr></table></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family:Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>If that doesn't work, contact to our: <a href={Url.Page("Contact")} target='_blank'>Contact</a></p></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 12px 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf'><p style='margin: 0;'>Cheers,<br>Recipe Organizer</p></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>You received this email because we received a request for verify for your account. If you didn't request registered you can safely delete this email.</p></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>Thu Duc city, Ho Chi Minh city</p></td></tr></table></td></tr></table></body></html>");

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }
    }
}
