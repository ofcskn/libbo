using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string School { get; set; }
        public bool TermConfirm { get; set; }
        public string EducationStatus { get; set; }
    }
}
