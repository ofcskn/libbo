using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
    }
}
