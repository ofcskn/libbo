using Entities.Context;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using www.Services.Abstract;

namespace www.Services.Concrete.EntityFramework
{
    public class EfContactService : EfGenericService<Contact>, IContactService
    {
        public EfContactService(DatabaseContext _context) : base(_context)
        {
        }
    }
}
