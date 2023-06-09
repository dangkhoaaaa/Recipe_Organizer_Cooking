using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace RecipeOrganizer.Controllers
{
    public class UserController : Controller
    {
        private readonly Recipe_OrganizerContext _context;

        public UserController(Recipe_OrganizerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'Recipe_OrganizerContext.Users'  is null.");
            
        }
    }
}

//        // GET: User
//        public async Task<IActionResult> Index()
//        {
//              return _context.Users != null ? 
//                          View(await _context.Users.ToListAsync()) :
//                          Problem("Entity set 'Recipe_OrganizerContext.Users'  is null.");
//        }

//        // GET: User/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }

//        //HTTP get /Home/Register
//        [HttpGet]
//        public ActionResult Register()
//        {
//            //kojijijiji
//            return View();
//        }

//        //HTTP get /Home/Register
//        [HttpPost]
//        public ActionResult Register(User user)
//        {
//            user.Status = true;
//            user.Role = "Admin";
//            user.Email = "test";
//            _context.Users.Add(user);
//            _context.SaveChanges();
//            return RedirectToAction("Login");
//        }

//        //HTTP post /User/Login
//        [HttpGet]
//        public ActionResult Login()
//        {
//            //if (Session["User"] != null)
//            //{
//            //    return View("Index");
//            //}

//            return View();
//        }


//        [HttpPost]
//        public ActionResult Login(User user)
//        {
//            var username = user.Username;
//            var password = user.Password;
//            var userCheck = _context.Users.SingleOrDefault(x => x.Username.Equals(username)
//                                                    && x.Password.Equals(password));
//            if (userCheck != null)
//            {
//               // Session["User"] = userCheck;
//                return RedirectToAction("Index", "Home");
//            }
//            else
//            {
//                ViewBag.LoginFail = "Login fail. Invalid username or password";
//                return View("Login");
//            }
//        }



//        //[HttpGet]
//        //public ActionResult Logout()
//        //{
//        //    Session["User"] = null;
//        //    return RedirectToAction("Login", "Home");
//        //}


//        // POST: User/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Email,FirstName,LastName,Birthday,Avatar,Role,Status")] User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(user);
//        }

//        // GET: User/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return View(user);
//        }

//        // POST: User/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password,Email,FirstName,LastName,Birthday,Avatar,Role,Status")] User user)
//        {
//            if (id != user.UserId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(user);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserExists(user.UserId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(user);
//        }

//        // GET: User/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }

//        // POST: User/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Users == null)
//            {
//                return Problem("Entity set 'Recipe_OrganizerContext.Users'  is null.");
//            }
//            var user = await _context.Users.FindAsync(id);
//            if (user != null)
//            {
//                _context.Users.Remove(user);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserExists(int id)
//        {
//          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
//        }
//    }
//}
