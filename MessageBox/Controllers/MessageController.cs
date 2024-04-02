using BusinessLayer.Abstracts;
using BusinessLayer.Concretes;
using BusinessLayer.ValidationRules.AppUserValidationRules;
using BusinessLayer.ValidationRules.MessageValidationRules;
using EntityLayer.Concrete;
using FluentValidation;
using Humanizer;
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
            AppUser user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
                return new AppUser();
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
            var validator = new SendMessageValidator();
            var validationResult = validator.Validate(message);
            if (validationResult.IsValid)
            {
                var user = await GetCurrentUserAsync();

                message.AppUserId = user.Id;
                message.SendDate = DateTime.Now;
                message.Status = true;
                message.IsRead = false;
                message.IsImportant = false;
                message.IsTrash = false;

                _messageService.TInsert(message);

                return RedirectToAction("Index");
            }
            return View(message);

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
                return RedirectToAction("MessageDetail", new { id = message.MessageId });
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
        public async Task<IActionResult> ImportantMessages()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetImportantMessagesListWithSender(user.Email);
            return View(messages);
        }
        public async Task<IActionResult> TrashMessages()
        {
            var user = await GetCurrentUserAsync();
            var messages = _messageService.TGetTrashMessagesListWithSender(user.Email);
            return View(messages);
        }

        public IActionResult SendTrash(int id)
        {
            _messageService.TSendTrash(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMessage(int id)
        {
            _messageService.TDelete(id);
            return RedirectToAction("TrashMessages");
        }
        public IActionResult MakeImportant(int id)
        {
            var message = _messageService.TGetById(id);
            if (message == null)
            {
                return NotFound();
            }
            if (message.IsImportant)
            {
                message.IsImportant = false;
                _messageService.TUpdate(message);
                return RedirectToAction("Index");
            }
            else
            {
                message.IsImportant = true;
                _messageService.TUpdate(message);
                return RedirectToAction("Index");
            }
        }
        public IActionResult MakeStatus(int id)
        {
            var message = _messageService.TGetById(id);
            if (message == null)
            {
                return NotFound();
            }
            if (message.Status)
            {
                message.Status = false;
                _messageService.TUpdate(message);
                return RedirectToAction("SentMessages");
            }
            return RedirectToAction("SentMessages");

        }
    }
}
