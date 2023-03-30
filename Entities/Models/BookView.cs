using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class BookView
    {
        public string AuthorSurname { get; set; }
        public string CategoryName { get; set; }
        public string PrintingYear { get; set; }
        public int PageNo { get; set; }
        public string Printery { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        public int AuthorId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string Score { get; set; }
        public int? ReadCount { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
