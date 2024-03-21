using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessageBox.Controllers
{
    public class MessageLayoutController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public MessageLayoutController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult _Layout()
        {
            return View();
        }

    }
}
