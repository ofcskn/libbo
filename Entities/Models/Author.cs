using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? DeathDay { get; set; }
        public string Biography { get; set; }
        public string Image { get; set; }
        public string HomeTown { get; set; }
        public string Country { get; set; }
        public string Score { get; set; }
    }
}
