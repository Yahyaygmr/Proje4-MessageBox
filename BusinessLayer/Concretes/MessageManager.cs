using BusinessLayer.Abstracts;
using DataAccessLayer.Abstracts;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concretes
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void TDelete(int id)
        {
            _messageDal.Delete(id);
        }

        public List<Message> TGetAllMessagesListWithSender(string email)
        {
            return _messageDal.GetAllMessagesListWithSender(email);
        }

        public Message TGetById(int id)
        {
            return _messageDal.GetById(id);
        }

        public List<Message> TGetIncomingMessagesListWithSender(string email)
        {
            return _messageDal.GetIncomingMessagesListWithSender(email);
        }

        public List<Message> TGetListAll()
        {
            return _messageDal.GetListAll();
        }

        public Message TGetMessageByIdWithSender(int id)
        {
            return _messageDal.GetMessageByIdWithSender(id);
        }

        public List<Message> TGetSentMessagesListWithSender(string email)
        {
            return _messageDal.GetSentMessagesListWithSender(email);
        }

        public List<Message> TGetTrashMessagesListWithSender(string email)
        {
            return _messageDal.GetTrashMessagesListWithSender(email);
        }

        public void TInsert(Message entity)
        {
            _messageDal.Insert(entity);
        }

        public void TStatusMakeFalse(int id)
        {
            _messageDal.StatusMakeFalse(id);
        }

        public void TUpdate(Message entity)
        {
            _messageDal.Update(entity);
        }
    }
}
