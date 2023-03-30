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
    public class EfMemberService:EfGenericService<Member>, IMemberService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EfMemberService(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) : base(_context)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
        public string GetNameByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "name").Value; }
        }
        public string GetIdByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id").Value; }
        }
        public string GetImageByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "image").Value; }
        }

        public string GetTypeByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "role").Value; }
        }
        public string GetMailByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "mail").Value; }
        }
        public Member GetMember(string username, string password)
        {
            return _db.Member.FirstOrDefault(p => p.UserName == username && p.Password == password);
        }
    }
}
