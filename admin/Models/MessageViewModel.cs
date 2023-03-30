using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Models
{
    public class MessageViewModel
    {
        public IEnumerable<MessageView> MessageViews { get; set; }
        public Message Message { get; set; }
        public IEnumerable<ThreadView> Threads { get; set; }
        public Thread Thread { get; set; }
    }
}
