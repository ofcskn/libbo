using Entities.Context;
using Entities.Models;
using admin.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace admin.Services.Concrete.EntityFramework
{
    public class EfAdminService : EfGenericService<Admin>, IAdminService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EfAdminService(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) : base(_context)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }

        public string GetNameByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "name").Value; }
        }
        public string GetImageByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "image").Value; }
        }

        public string GetTypeByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "role").Value; }
        }

        public string GetMailByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "mail").Value; }
        }

        public string GetIdByAdmin
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id").Value; }
        }

        public Admin IsAdmin(int id)
        {
            var all = _db.Admin.FirstOrDefault(p => p.IsAdmin == true);
            all.IsAdmin = false;
            var admin = GetById(id);
            admin.IsAdmin = true;
            Update(admin);
            return admin;
        }
        public Admin GetAdmin(string username, string password)
        {
            return _db.Admin.FirstOrDefault(p => p.UserName == username && p.Password == password);
        }
    }
}
