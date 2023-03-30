using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int MemberId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReadDate { get; set; }
        public int ThreadId { get; set; }
        public bool Seen { get; set; }
        public string IpAdress { get; set; }
    }
}
