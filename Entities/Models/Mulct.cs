using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Mulct
    {
        public int Id { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public decimal? Money { get; set; }
        public string Detail { get; set; }
        public int MemberId { get; set; }
        public int? ProcessId { get; set; }
    }
}
