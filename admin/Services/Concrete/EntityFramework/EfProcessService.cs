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
    public class EfProcessService : EfGenericService<Process>, IProcessService
    {
        public EfProcessService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }

        public IQueryable<ProcessView> GetAllByView()
        {
            return _db.ProcessView.OrderByDescending(p => p.Enabled ).ThenByDescending(p=>p.ReturnItDate);
        }
    }
}
