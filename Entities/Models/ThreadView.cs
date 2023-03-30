using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ThreadView
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string School { get; set; }
        public int CreateMemberId { get; set; }
        public int ReceiverMemberId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
    }
}
