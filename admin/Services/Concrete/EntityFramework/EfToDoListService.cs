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
    public class EfToDoListService : EfGenericService<ToDoList>, IToDoListService
    {
        IHttpContextAccessor _httpContextAccessor;
        public EfToDoListService(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) : base(_context)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
        public string GetTypeByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "role").Value; }
        }

        public string GetIdByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id").Value; }
        }

        public IQueryable<ToDoList> GetAllByEnabled()
        {
            return _db.ToDoList.Where(p=>p.Enabled == false).OrderByDescending(p=>p.Date).Take(7);
        }
    }
}
