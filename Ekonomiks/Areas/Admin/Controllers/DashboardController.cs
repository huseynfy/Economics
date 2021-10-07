using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ekonomiks.DAL;
using Ekonomiks.Helpers;
using Ekonomiks.Models;
using Ekonomiks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ekonomiks.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class DashboardController : Controller
    {
        private readonly AppDbContext db;
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        public DashboardController(UserManager<AppUser> _usermanager, SignInManager<AppUser> _signInManager
            , RoleManager<IdentityRole> _rolemanager, AppDbContext dbs)
        {
            userManager = _usermanager;
            signInManager = _signInManager;
            roleManager = _rolemanager;
            db = dbs;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            ViewBag.MessageCount = db.Messages.Count();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task RoleSeed()
        {
            if (!await roleManager.RoleExistsAsync(UR.Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(UR.Roles.Admin.ToString()));
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result = await userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await userManager.AddToRoleAsync(user, UR.Roles.Admin.ToString());
            await signInManager.SignInAsync(user, true);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AppUser user = await userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(login);
            }
            SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(login);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}