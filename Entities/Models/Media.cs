using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string FileNames { get; set; }
        public DateTime Date { get; set; }
        public bool IsSlider { get; set; }
    }
}
