using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using Services.Repository;
using Services.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Firebase.Auth;
using System.Collections.Generic;
using RecipeOrganizer.Areas.Admin.Models.Manage;
using System.Security.Claims;

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


            var result = await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: true);
            // Tìm UserName theo Email, đăng nhập lại
            if ((!result.Succeeded) && AppUtilities.IsValidEmail(model.UserNameOrEmail))
            {
                var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                if (user != null)
                {
                    result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
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

        [HttpGet("/Admin/")]
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
            //get all user
            foreach (var user in listUser)
            {
                var role = await _userManager.GetRolesAsync(user);
                var isLockout = await _userManager.IsLockedOutAsync(user);
                var externalLogins = (List<UserLoginInfo>) await _userManager.GetLoginsAsync(user);
                var model = new IndexViewModel
                {
                    Member = user,
                    Role = role.ToList(),
                    TotalRecipe = _recipeRepository.GetByAuthor(user.Id).Count,
                    //TotalRecipe = 2,
                    Status = !isLockout,
                    ExternalLogin = externalLogins,
                };
                list.Add(model);
            }
            return View(list);
        }

        private Task<AppUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserStatus(string userID, string searchUser)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);
                var currentUser = await GetCurrentUserAsync();
                if (userID.Equals(currentUser.Id))
                {
                    TempData["UpdateError"] = "You can not update yourself";
                    return RedirectToAction("Index");
                }
                if (role.Contains(RoleName.Administrator))
                {
                    TempData["UpdateError"] = "You can not update another admin";
                    return RedirectToAction("Index");
                }
                var isLockedOut = await _userManager.IsLockedOutAsync(user);
                if (isLockedOut)
                {
                    //user.LockoutEnabled = false;
                    user.LockoutEnd = null;
                } else
                {
                    //user.LockoutEnabled = true;
                    user.LockoutEnd = DateTimeOffset.MaxValue;
                }
                await _userManager.UpdateAsync(user);
                return RedirectToAction("SearchUser", new {keyword = searchUser});
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> SearchUser(string keyword)
        {
            ViewBag.Keyword = keyword;
            List<AppUser> listAllUsers = new List<AppUser>(_userManager.Users);
            List<IndexViewModel> index = new List<IndexViewModel>();
            if (keyword == null)
            {
                ViewBag.NotFind = "";
                return RedirectToAction("Index");
            }
            var listSearchUser = listAllUsers.Where(p => p.Email.Contains(keyword.Trim()) || p.UserName.Contains(keyword.Trim())).ToList();
            if (listSearchUser.Count == 0)
            {

                ViewBag.NotFind = "No result match the keyword";
                return RedirectToAction("Index");
            }
            foreach (var userSearch in listSearchUser)
            {
                var role = await _userManager.GetRolesAsync(userSearch);
                var isLockout = await _userManager.IsLockedOutAsync(userSearch);
                index.Add(new IndexViewModel
                {
                    Member = userSearch,
                    Role = role.ToList(),
                    TotalRecipe = _recipeRepository.GetByAuthor(userSearch.Id).Count,
                    //TotalRecipe = 2,
                    Status = !isLockout
                });
            }
            return View("Index", index);
        }

        //GET: Admin/UserRecipe/{id}
        public async Task<IActionResult> UserRecipe(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                ViewBag.UserError = "Can not find user";
                return View("Index");
            }
            var listUserRecipe = _recipeRepository.GetByAuthor(userID);
            var model = new UserRecipeViewModel {
                User = user,
                UserRecipe = listUserRecipe,
                TotalRecipe = listUserRecipe.Count(),
            };
            if (listUserRecipe == null)
                ViewBag.NoRecipe = "No recipe";
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CreateAdminAsync()
        {
            // admin, pass=admin123, admin@example.com
            var useradmin = await _userManager.FindByEmailAsync("recipeorganizert3@gmail.com");
            if (useradmin == null)
            {
                useradmin = new AppUser()
                {
                    UserName = "adminabc",
                    Email = "recipeorganizert3@gmail.com",
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(useradmin, "Admin123");
                await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
                await _signInManager.SignInAsync(useradmin, false);
                return RedirectToAction("Login");
            }
            //SeedPostCategory();
            //SeedProductCategory();

            //StatusMessage = "Vừa seed Database";
            return RedirectToAction("Login");
        }

        //public async Task<IActionResult> SeedDataAsync()
        //{
        //    // Create Roles
        //    var rolenames = typeof(RoleName).GetFields().ToList();
        //    foreach (var r in rolenames)
        //    {
        //        var rolename = (string)r.GetRawConstantValue();
        //        var rfound = await _roleManager.FindByNameAsync(rolename);
        //        if (rfound == null)
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole(rolename));
        //        }
        //    }
        //    // admin, pass=admin123, admin@example.com
        //    var useradmin = await _userManager.FindByEmailAsync("recipeorganizert3@gmail.com");
        //    if (useradmin == null)
        //    {
        //        useradmin = new AppUser()
        //        {
        //            UserName = "admin",
        //            Email = "recipeorganizert3@gmail.com",
        //            EmailConfirmed = true,
        //        };
        //        await _userManager.CreateAsync(useradmin, "admin123");
        //        await _userManager.AddToRoleAsync(useradmin, RoleName.Administrator);
        //        await _signInManager.SignInAsync(useradmin, false);

        //        return RedirectToAction("SeedData");
        //    }
        //    else
        //    {
        //        var user = await _userManager.GetUserAsync(this.User);
        //        if (user == null) return this.Forbid();
        //        var roles = await _userManager.GetRolesAsync(user);

        //        if (!roles.Any(r => r == RoleName.Administrator))
        //        {
        //            return this.Forbid();
        //        }
        //    }
        //    //SeedPostCategory();
        //    //SeedProductCategory();

        //    //StatusMessage = "Vừa seed Database";
        //    return RedirectToAction("Index");
        //}

        //
        // POST: /Manage/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var redirectUrl = Url.Action("ExternalLoginCallback", "Admin", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        //
        // GET: /Manage/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

			var emailReg = info.Principal.FindFirstValue(ClaimTypes.Email);
			//-----------------------------------------------------------------------------------//
			//email luc nhap confirm so sanh xem da co ton tai trong DB chua
			var userReg = await _userManager.FindByEmailAsync(emailReg);
			//chỉ có admin mới được liên kết
			var roleReq = await _userManager.GetRolesAsync(userReg);
			if (!roleReq.Contains(RoleName.Administrator))
			{
				return View("AccessDenied");
			}

			// Sign in the user with this external login provider if the user already has a login.
			var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                //update login time
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                user.LastLoginTime = DateTime.Now;
                await _userManager.UpdateAsync(user);

                // Cập nhật lại token
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
				//return LocalRedirect(returnUrl);
				return RedirectToAction("Index");
			}
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //-----------------------------------------------------------------------------------//
                //email luc nhap confirm so sanh xem da co ton tai trong DB chua
                var registeredUser = await _userManager.FindByEmailAsync(email);
                string externalEmail = null;
                AppUser externalEmailUser = null;

                // Claim ~ Dac tinh mo ta mot doi tuong 
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    externalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                //email cua external
                if (externalEmail != null)
                {
                    externalEmailUser = await _userManager.FindByEmailAsync(externalEmail);
                }
                //chỉ có admin mới được liên kết
                var role = await _userManager.GetRolesAsync(registeredUser);
                if (!role.Contains(RoleName.Administrator))
                {
                    return View("AccessDenied");
                }

                //email da dang ki vao app va email external cung da dang ki vao app
                if ((registeredUser != null) && (externalEmailUser != null))
                {
                    // externalEmail  == Input.Email
                    // email google or facebook == email da dang ky vao app
                    if (registeredUser.Id == externalEmailUser.Id)
                    {
                        // Lien ket tai khoan, dang nhap
                        var resultLink = await _userManager.AddLoginAsync(registeredUser, info);
                        if (resultLink.Succeeded)
                        {
                            //await _userManager.AddLoginAsync(registeredUser, info);
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
                            await _userManager.ConfirmEmailAsync(registeredUser, code);

                            await _signInManager.SignInAsync(registeredUser, isPersistent: false);
                            registeredUser.LastLoginTime = DateTime.Now; // Update the LastLoginTime property
                            await _userManager.UpdateAsync(registeredUser); // Save the changes to the user entity
                            //return LocalRedirect(returnUrl);
                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        // registeredUser = externalEmailUser (externalEmail != Input.Email)
                        /*
                            info => user1 (mail1@abc.com)
                                 => user2 (mail2@abc.com)
                        */
                        ModelState.AddModelError(string.Empty, "Account cannot be linked, please use another email account");
                        return View("ExternalLoginFailure");
                    }

                }
                return View("AccessDenied");
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            } //end else
        }

        //
        // GET: /Admin/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Admin/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User changed their password successfully.");
                    //return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                    _logger.LogInformation(3, "User changed their password successfully.");
                    await _signInManager.SignOutAsync();
                    _logger.LogInformation("User logged out");
                    @TempData["ChangePasswordSuccess"] = "Change password success. You must login again!";
                    return Redirect("/Admin/Login");
                }
                TempData["ChangeError"] = "Incorrect password";
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }


    }



}
