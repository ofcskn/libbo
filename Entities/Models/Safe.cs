using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Safe
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}
