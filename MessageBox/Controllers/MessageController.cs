using BusinessLayer.Abstracts;
using BusinessLayer.Concretes;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MessageBox.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMessageService _messageService;

        public MessageController(UserManager<AppUser> userManager, IMessageService messageService)
        {
            _userManager = userManager;
            _messageService = messageService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messages = _messageService.TGetListAll();
            return View(messages);
        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            message.AppUserId = user.Id;
            message.SendDate = DateTime.Now;
            message.Status = true;
            message.IsRead = false;

            _messageService.TInsert(message);

            return RedirectToAction("Index");
        }
        public IActionResult MessageDetail(int id)
        {
            var message = _messageService.TGetById(id);
            return View(message);
        }
        public IActionResult MessageControl(int id)
        {
            var message = _messageService.TGetById(id);

            if (message.IsRead)
            {
                return RedirectToAction("MessageDetail", id);
            }
            else
            {
                message.IsRead = true;
                _messageService.TUpdate(message);
                return RedirectToAction("MessageDetail", id);
            }
        }
    }
}
