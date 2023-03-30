using admin.Services.Abstract;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Concrete.EntityFramework
{
    public class EFMulctService : EfGenericService<Mulct>, IMulctService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EFMulctService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
        public IQueryable<MulctView> GetAllInView()
        {
            return _db.MulctView.OrderByDescending(p=>p.StartingDate);
        }
    }
}
