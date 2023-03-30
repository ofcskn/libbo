using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Process
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int StaffId { get; set; }
        public DateTime GettingDate { get; set; }
        public DateTime ReturningDate { get; set; }
        public DateTime ReturnItDate { get; set; }
        public bool Enabled { get; set; }
    }
}
