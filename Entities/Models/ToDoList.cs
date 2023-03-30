using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ToDoList
    {
        public int Id { get; set; }
        public string Goal { get; set; }
        public DateTime Date { get; set; }
        public string Role { get; set; }
        public int PersonId { get; set; }
        public bool Enabled { get; set; }
    }
}
