using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime DateOfStart { get; set; }
        public string Score { get; set; }
        public int? Price { get; set; }
    }
}
