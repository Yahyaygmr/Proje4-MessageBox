using BusinessLayer.ValidationRules.AppUserValidationRules;
using DTO.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessageBox.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EditAppUserPasswordDto dto = new EditAppUserPasswordDto();
            dto.Name = user.Name;
            dto.Surname = user.Surname;
            dto.Email = user.Email;
            dto.UserName = user.UserName;
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(EditAppUserPasswordDto dto)
        {
            EditAppUserPasswordValidator validator = new EditAppUserPasswordValidator();
           var validationResult = validator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user =await _userManager.FindByNameAsync(User.Identity.Name);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,dto.Password);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
           return View(dto);
        }
    }
}
