using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MailMessage
    {
        public int Id { get; set; }
        public string SenderMail { get; set; }
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public bool Seen { get; set; }
    }
}
