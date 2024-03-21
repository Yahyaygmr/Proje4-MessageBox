using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string RecieverMail { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Status { get; set; }
        public bool IsRead { get; set; }
    }
}
