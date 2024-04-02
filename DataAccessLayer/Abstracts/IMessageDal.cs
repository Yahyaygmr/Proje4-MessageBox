using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstracts
{
    public interface IMessageDal : IGenericDal<Message>
    {
        List<Message> GetAllMessagesListWithSender(string email);
        List<Message> GetIncomingMessagesListWithSender(string email);
        List<Message> GetImportantMessagesListWithSender(string email);
        List<Message> GetSentMessagesListWithSender(string email);
        List<Message> GetTrashMessagesListWithSender(string email);
        Message GetMessageByIdWithSender(int messageId);
        void SendTrash(int messageId);
    }
}
