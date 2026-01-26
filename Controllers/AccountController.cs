using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using finchInteligent.Models;
using finchInteligent.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace finchInteligent.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuário criado com sucesso.");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Se deu erro, joga os erros do Identity pro ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Login(LoginViewModels models)
        {
            if (!ModelState.IsValid)
                return View(models);
            var result = await _signInManager.PasswordSignInAsync(
                models.Email,
                models.Password,
                models.RememberMe,
                lockoutOnFailure: false
            );
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            return View(models);



        }
    }
}