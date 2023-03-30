using admin.Services.Abstract;
using Entities.Context;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Concrete.EntityFramework
{
    public class EfBookService:EfGenericService<Book>, IBookService
    {
        public EfBookService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }

        public IQueryable<BookView> GetAllInView()
        {
            return _db.BookView;
        }

        public IQueryable<BookView> GetByTopRead()
        {
            return _db.BookView.OrderByDescending(p => p.ReadCount);
        }
        public IQueryable<BookView> GetByTopScore()
        {
            return _db.BookView.OrderByDescending(p => p.Score);
        }
    }
}
