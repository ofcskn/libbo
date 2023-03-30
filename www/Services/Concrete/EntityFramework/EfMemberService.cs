using www.Services.Abstract;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Services.Concrete.EntityFramework
{
    public class EfMemberService:EfGenericService<Member>, IMemberService
    {
        public EfMemberService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }

    }
}
