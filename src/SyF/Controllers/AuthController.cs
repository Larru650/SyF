using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SyF.Models;
using SyF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<SyFUser> _signInManager;

        public AuthController(SignInManager<SyFUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Recipes", "App");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid) //if LoginViewModel is valid
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {

                        return RedirectToAction("Recipes", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {

                    ModelState.AddModelError("", "Password or Email incorrect");
                }

            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            //we get rid of the cookies collection and we logout the user
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

    }
}
