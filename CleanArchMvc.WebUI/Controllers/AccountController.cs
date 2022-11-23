using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;

        public AccountController(IAuthenticate authentication)
        {
            _authentication = authentication;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var account = new RegisterFormViewModel();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormViewModel register)
        {
            
            var result = await _authentication.RegisterUser(register.Email, register.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt");
                return View(register);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {

            var result = await _authentication.Authenticate(login.Email, login.Password);
            if(result)
            {
                if(string.IsNullOrEmpty(login.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(login.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(login);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Login");
        }


    }
}
