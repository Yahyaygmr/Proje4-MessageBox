using DataAccessLayer.Abstracts;
using DataAccessLayer.Concretes.Context;
using DataAccessLayer.Concretes.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concretes.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        private readonly ApplicationContext _context;
        public EfMessageDal(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public List<Message> GetIncomingMessagesListWithSender(string email)
        {
            return _context.Messages
                .Include(x => x.AppUser)
                .Where(x => x.Status == true && x.RecieverMail == email)
                .OrderByDescending(x => x.MessageId)
                .ToList();
        }

        public List<Message> GetAllMessagesListWithSender(string email)
        {
            return _context.Messages
                .Include(x => x.AppUser)
                .Where(x => x.RecieverMail == email && x.Status == true)
                .OrderByDescending(x => x.MessageId)
                .ToList();

        }

        public List<Message> GetSentMessagesListWithSender(string email)
        {
            return _context.Messages
                .Include(x => x.AppUser)
                .Where(x => x.Status == true && x.AppUser.Email == email)
                .OrderByDescending(x => x.MessageId)
                .ToList();
        }

        public List<Message> GetTrashMessagesListWithSender(string email)
        {
            return _context.Messages
                .Include(x => x.AppUser)
                .Where(x => x.RecieverMail == email && x.Status == false)
                .OrderByDescending(x => x.MessageId)
                .ToList();
        }

        public Message GetMessageByIdWithSender(int id)
        {
            return _context.Messages
                .Include(x => x.AppUser)
                .FirstOrDefault(x => x.MessageId == id);
        }

        public void StatusMakeFalse(int id)
        {
            var message = _context.Messages.Find(id);

            message.Status = false;

            _context.Messages.Update(message);
            _context.SaveChanges();
        }
    }
}
