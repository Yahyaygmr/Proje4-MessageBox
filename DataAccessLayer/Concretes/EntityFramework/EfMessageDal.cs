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
				.Where(x => x.IsTrash == false && x.RecieverMail == email)
				.OrderByDescending(x => x.MessageId)
				.ToList();
		}

		public List<Message> GetAllMessagesListWithSender(string email)
		{
			return _context.Messages
				.Include(x => x.AppUser)
				.Where(x => (x.RecieverMail == email || x.AppUser.Email == email) && x.IsTrash == false && x.Status == true)
				.OrderByDescending(x => x.MessageId)
				.ToList();

		}

		public List<Message> GetSentMessagesListWithSender(string email)
		{
			return _context.Messages
				.Include(x => x.AppUser)
				.Where(x => x.IsTrash == false && x.AppUser.Email == email && x.Status == true)
				.OrderByDescending(x => x.MessageId)
				.ToList();
		}

		public List<Message> GetTrashMessagesListWithSender(string email)
		{
			return _context.Messages
				.Include(x => x.AppUser)
				.Where(x => x.RecieverMail == email && x.IsTrash == true)
				.OrderByDescending(x => x.MessageId)
				.ToList();
		}

		public Message GetMessageByIdWithSender(int messageId)
		{
			return _context.Messages
				.Include(x => x.AppUser)
				.FirstOrDefault(x => x.MessageId == messageId);
		}
		public List<Message> GetImportantMessagesListWithSender(string email)
		{
			return _context.Messages
				.Include(x => x.AppUser)
				.Where(x => x.RecieverMail == email && x.IsImportant == true)
				.OrderByDescending(x => x.MessageId)
				.ToList();
		}

		public void SendTrash(int messageId)
		{
			var message = _context.Messages.Find(messageId);
			if (message != null)
			{
				message.IsTrash = true;
				message.DeletedDate = DateTime.Now;

				_context.Messages.Update(message);
				_context.SaveChanges();
			}
		}
	}
}
