using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public string JsonData { get; set; }
        public int WebsiteId { get; set; }
        public string Lang { get; set; }
    }
}
