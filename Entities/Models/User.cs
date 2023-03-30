using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public bool Confirmation { get; set; }
    }
}
