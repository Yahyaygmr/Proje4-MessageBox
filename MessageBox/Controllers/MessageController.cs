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

        internal async Task<AppUser> GetCurrentUserAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
            }
            return user;
        }
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetAllMessagesListWithSender(user.Email);
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
            var user = await GetCurrentUserAsync();

            message.AppUserId = user.Id;
            message.SendDate = DateTime.Now;
            message.Status = true;
            message.IsRead = false;

            _messageService.TInsert(message);

            return RedirectToAction("Index");
        }
        public IActionResult MessageDetail(int id)
        {
            var message = _messageService.TGetMessageByIdWithSender(id);
            return View(message);
        }
        public IActionResult MessageControl(int id)
        {
            var message = _messageService.TGetById(id);

            if (message.IsRead)
            {
                return RedirectToAction("MessageDetail", new { id = message.MessageId });
            }
            else
            {
                message.IsRead = true;
                _messageService.TUpdate(message);
                return RedirectToAction("MessageDetail",new { id = message.MessageId });
            }
        }

        public async Task<IActionResult> IncomingMessages()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetIncomingMessagesListWithSender(user.Email);
            return View(messages);
        }
        public async Task<IActionResult> SentMessages()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetSentMessagesListWithSender(user.Email);
            return View(messages);
        }
        public async Task<IActionResult> TrashMessages()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetTrashMessagesListWithSender(user.Email);
            return View(messages);
        }

        public IActionResult StatusMakeFalse(int id)
        {
            _messageService.TStatusMakeFalse(id);
            return RedirectToAction("Index");
        }
    }
}
