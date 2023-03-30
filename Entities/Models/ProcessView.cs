using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ProcessView
    {
        public DateTime GettingDate { get; set; }
        public DateTime ReturningDate { get; set; }
        public int StaffId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public int Id { get; set; }
        public string StaffName { get; set; }
        public string MemberName { get; set; }
        public string BookName { get; set; }
        public string MemberSurname { get; set; }
        public string StaffSurname { get; set; }
        public bool Enabled { get; set; }
        public DateTime ReturnItDate { get; set; }
    }
}
