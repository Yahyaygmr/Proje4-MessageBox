using BusinessLayer.Abstracts;
using BusinessLayer.ValidationRules.AppUserValidationRules;
using DTO.AppUserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageBox.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IAppUserService _appUserService;

        public RegisterController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterAppUserDto dto)
        {
            var validator = new RegisterAppUserValidator();
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                await _appUserService.UserRegisterAsync(dto);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

            }
            return View(dto);
        }
    }
}
