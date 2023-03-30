﻿using www.Services.Abstract;
using Entities.Context;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Services.Concrete.EntityFramework
{
    public class EfCategoryService:EfGenericService<Category>, ICategoryService
    {
        public EfCategoryService(DatabaseContext _context) : base(_context)
        {
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
    }
}
