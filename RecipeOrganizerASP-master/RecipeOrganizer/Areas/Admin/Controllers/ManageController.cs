using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using RecipeOrganizer.Areas.Admin.Models;
using Services.Models;
using Services.Models.Authentication;
using System;
using Services.Repository;
using RecipeOrganizer.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Firebase.Auth;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    [Area("Admin")]
	[Route("/Admin/[action]")]
	public class ManageController : Controller
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IEmailSender _emailSender;
        private readonly ILogger<ManageController> _logger;

        private UserRepository _userRepository;
        private RecipeRepository _recipeRepository;


        public ManageController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender,
        ILogger<ManageController> logger        
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _logger = logger;
            _recipeRepository = new RecipeRepository();
            _userRepository = new UserRepository();
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        [HttpGet("/manage/")]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Password has change"
                : message == ManageMessageId.SetPasswordSuccess ? "Password has reset"
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "Error"
                : message == ManageMessageId.AddPhoneSuccess ? "Phone number has added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Phone number has removed."
                : "";

            var listUser = _userRepository.GetAll();
            List<IndexViewModel> list = new List<IndexViewModel>();

            foreach (var user in listUser)
            {
                var role = await _userManager.GetRolesAsync(user);
                var model = new IndexViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = role.ToList(),
                    TotalRecipe = 2,
                    Status = user.Status
                };
                list.Add(model);
            }
            return View(list);
        }

        private Task<AppUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: /Admin/Login
        //[HttpGet("/login/")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            //user already login
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        //[HttpPost("/login/")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;


            var result = await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: false);
            // Tìm UserName theo Email, đăng nhập lại
            if ((!result.Succeeded) && AppUtilities.IsValidEmail(model.UserNameOrEmail))
            {
                var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                if (user != null)
                {
                    result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                }
            }

            if (result.Succeeded)
            {
                //tìm user theo email truyền tới
                _logger.LogInformation(1, "User logged in.");
                return RedirectToAction("Index");
            }
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //}

            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, "Account was banned.");
                return View("Lockout");
            }
            else
            {
                return View(model);
            }
            //return View(model);
        }

        // POST: /Account/LogOut
        //[HttpPost("/logout/")]
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserStatus(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {
                if (user.Status)
                {
                    user.Status = false;
                }
                else
                {
                    user.Status = true;
                }
                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index");

            }
            return RedirectToAction("LogOut");

        }




        ////
        //// GET: /Account/SendCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        //{
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}
        //
        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    // Dùng mã Authenticator
        //    if (model.SelectedProvider == "Authenticator")
        //    {
        //        return RedirectToAction(nameof(VerifyAuthenticatorCode), new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //    }

        //    // Generate the token and send it
        //    var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
        //    if (string.IsNullOrWhiteSpace(code))
        //    {
        //        return View("Error");
        //    }

        //    var message = "Your security code is: " + code;
        //    if (model.SelectedProvider == "Email")
        //    {
        //        await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
        //    }
        //    else if (model.SelectedProvider == "Phone")
        //    {
        //        await _emailSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
        //    }

        //    return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}
        ////
        //// GET: /Account/VerifyCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    model.ReturnUrl ??= Url.Content("~/");
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes.
        //    // If a user enters incorrect codes for a specified amount of time then the user account
        //    // will be locked out for a specified amount of time.
        //    var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
        //    if (result.Succeeded)
        //    {
        //        return LocalRedirect(model.ReturnUrl);
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        _logger.LogWarning(7, "User account locked out.");
        //        return View("Lockout");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid code.");
        //        return View(model);
        //    }
        //}

        ////
        //// GET: /Account/VerifyAuthenticatorCode
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string returnUrl = null)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyAuthenticatorCodeViewModel { ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/VerifyAuthenticatorCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorCodeViewModel model)
        //{
        //    model.ReturnUrl ??= Url.Content("~/");
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes.
        //    // If a user enters incorrect codes for a specified amount of time then the user account
        //    // will be locked out for a specified amount of time.
        //    var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, model.RememberBrowser);
        //    if (result.Succeeded)
        //    {
        //        return LocalRedirect(model.ReturnUrl);
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        _logger.LogWarning(7, "User account locked out.");
        //        return View("Lockout");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Wrong code.");
        //        return View(model);
        //    }
        //}

        [AllowAnonymous]
        public async Task<IActionResult> CreateAdminAsync()
        {
            // admin, pass=admin123, admin@example.com
            var useradmin = await _userManager.FindByEmailAsync("recipeorganizert3@gmail.com");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "recipeorganizert3@gmail.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);
                return RedirectToAction("Login");
            }
            //SeedPostCategory();
            //SeedProductCategory();

            //StatusMessage = "Vừa seed Database";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            // Create Roles
            var rolenames = typeof(RoleName).GetFields().ToList();
            foreach (var r in rolenames)
            {
                var rolename = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(rolename);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(rolename));
                }
            }
            // admin, pass=admin123, admin@example.com
            var useradmin = await _userManager.FindByEmailAsync("recipeorganizert3@gmail.com");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "recipeorganizert3@gmail.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(useradmin, "admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);

                return RedirectToAction("SeedData");
            }
            else
            {
                var user = await _userManager.GetUserAsync(this.User);
                if (user == null) return this.Forbid();
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Any(r => r == RoleName.Administrator))
                {
                    return this.Forbid();
                }
            }
            //SeedPostCategory();
            //SeedProductCategory();

            //StatusMessage = "Vừa seed Database";
            return RedirectToAction("Index");
        }
    }

}
