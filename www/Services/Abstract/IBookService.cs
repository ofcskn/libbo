using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Services.Abstract
{
    public interface IBookService:IGenericService<Book>
    {
        IQueryable<BookView> GetAllInView();
        IQueryable<BookView> GetByTopRead();
        IQueryable<BookView> GetByTopScore();
        IQueryable<BookView> Get10Book();
    }
}
