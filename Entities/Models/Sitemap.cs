using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Sitemap
    {
        public string Permalink { get; set; }
        public string Type { get; set; }
        public string ChangeFrequency { get; set; }
        public decimal Priority { get; set; }
        public DateTime Modified { get; set; }
        public int RelatedId { get; set; }
        public int WebsiteId { get; set; }
    }
}
