using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.Services;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SqlDbContext _sqlDbContext;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SqlDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sqlDbContext = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Lastname = model.FirstName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var Buyer = new Buyer { User = user, FirstName= user.FirstName, LastName = user.Lastname, Email = user.Email };
                    
                    using (_sqlDbContext)
                    {
                        _sqlDbContext.Buyers.Add(Buyer);
                        await _sqlDbContext.SaveChangesAsync();

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        
                    }

                    


                    return RedirectToAction("SPList", "SailPoints");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return RedirectToAction("SPList", "SailPoints");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await _signInManager.SignInAsync(user, model.RememberMe);
                        Helpers.SessionHelper.SetObjectAsJson(HttpContext.Session, "User", user);
                    }
                   
                    return RedirectToAction("SPList", "SailPoints");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
