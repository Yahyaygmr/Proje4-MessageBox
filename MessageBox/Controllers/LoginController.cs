using BusinessLayer.ValidationRules.AppUserValidationRules;
using DTO.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessageBox.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginAppUserDto dto)
        {
            var validator = new LoginAppUserValidator();
            var validationResult = validator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz mail veya şifre");
                    return View(dto);
                }
                var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Register");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz mail veya şifre");
                }
            }
           
            return View(dto);
        }
    }
}
