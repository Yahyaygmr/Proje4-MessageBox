using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstracts
{
    public interface IMessageService : IGenericService<Message>
    {
		List<Message> TGetAllMessagesListWithSender(string email);
		List<Message> TGetIncomingMessagesListWithSender(string email);
		List<Message> TGetImportantMessagesListWithSender(string email);
		List<Message> TGetSentMessagesListWithSender(string email);
		List<Message> TGetTrashMessagesListWithSender(string email);
		Message TGetMessageByIdWithSender(int messageId);
		void TSendTrash(int messageId);
	}
}
