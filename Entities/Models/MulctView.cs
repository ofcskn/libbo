using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MulctView
    {
        public int Id { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public decimal? Money { get; set; }
        public string Detail { get; set; }
        public int MemberId { get; set; }
        public int? ProcessId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime ReturningDate { get; set; }
    }
}
