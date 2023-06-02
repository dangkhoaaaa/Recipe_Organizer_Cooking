﻿using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Models;
using System.Diagnostics;

namespace RecipeOrganizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }


        //HTTP get /Home/Register
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Register page.";

            return View();
        }

        //HTTP get /Home/Register
        //[HttpPost]
        //public ActionResult Register(User user)
        //{
        //    user.status = true;
        //    user.role_id = 2;
        //    db.Users.Add(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Login");
        //}


        //HTTP post /Home/Login
        [HttpGet]
        public ActionResult Login()
        {
            //if (Session["User"] != null)
            //{
            //    return View("Index");
            //}

            return View();
        }

        /*
        [HttpPost]
        public ActionResult Login(User user)
        {
            
            var username = user.username;
            var password = user.password;
            var userCheck = db.Users.SingleOrDefault(x => x.username.Equals(username)
                                                    && x.password.Equals(password));
            if (userCheck != null)
            {
                Session["User"] = userCheck;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginFail = "Login fail, not valid username or password";
                return View("Login");
            }
        }



        [HttpGet]
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Home");
        }
        */
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}