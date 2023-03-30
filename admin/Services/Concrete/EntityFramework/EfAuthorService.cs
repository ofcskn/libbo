using admin.Services.Abstract;
using Entities.Context;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Concrete.EntityFramework
{
    public class EfAuthorService:EfGenericService<Author>, IAuthorService
    {
        public EfAuthorService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
    }
}
