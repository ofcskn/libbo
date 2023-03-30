using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Models
{
    public class BookViewModel
    {
        public IEnumerable<Book> Books;
        public Category Category;
        public Author Author;
    }
}
