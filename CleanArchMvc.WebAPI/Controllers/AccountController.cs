using backend.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return $"AutorizaController acessado em: {DateTime.Now.ToLongDateString()}";
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterFormViewModel register)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Erro de registro!");
            }

            var user = new ApplicationUser
            {
                Nome = register.Nome,
                SobreNome = register.SobreNome,
                UserName = register.Email,
                Email = register.Email,
                EmailConfirmed = true
            };


            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var tokenService = new TokenService(_config);

            return Ok(tokenService.GeraToken(user));

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Erro de login!");
            }

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password,
                isPersistent: false, lockoutOnFailure: false);

            var tokenService = new TokenService(_config);

            if (result.Succeeded)
            {
                var usuario = new ApplicationUser
                {
                    UserName = login.Email,
                    Email = login.Email
                };
                return Ok(new { token = tokenService.GeraToken(usuario) });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Invalido");
                return BadRequest(ModelState);
            }

        }

    }
}
