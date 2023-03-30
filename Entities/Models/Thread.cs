using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Thread
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateMemberId { get; set; }
        public int ReceiverMemberId { get; set; }
    }
}
