using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MessageView
    {
        public string Text { get; set; }
        public int MemberId { get; set; }
        public int ThreadId { get; set; }
        public bool Seen { get; set; }
        public string IpAdress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReadDate { get; set; }
    }
}
