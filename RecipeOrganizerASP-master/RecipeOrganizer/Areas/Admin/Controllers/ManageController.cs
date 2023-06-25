using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using Services.Repository;
using RecipeOrganizer.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Firebase.Auth;
using System.Collections.Generic;
using RecipeOrganizer.Areas.Admin.Models.Manage;

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserStatus(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user != null)
            {
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> SearchUser(string keyword)
        {
            ViewBag.Keyword = keyword;
            List<AppUser> listAllUsers = new List<AppUser>(_userManager.Users);
            List<IndexViewModel> index = new List<IndexViewModel>();
            if (keyword != null)
            {
                var listSearchUser = listAllUsers.Where(p => p.Email.Contains(keyword.Trim()) || p.UserName.Contains(keyword.Trim())).ToList();
                if (listSearchUser.Count == 0)
                {

                    ViewBag.NotFind = "No result match the keyword";
                    return View("Index");

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
            else
            {
                ViewBag.NotFind = "Invalid keyword";
            }
            return View("Index");
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
            var model = new UserReipceViewModel {
                User = user,
                UserRecipe = listUserRecipe,
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
