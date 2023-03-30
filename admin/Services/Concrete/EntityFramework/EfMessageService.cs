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
    public class EfMessageService : EfGenericService<Message>, IMessageService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public EfMessageService(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) : base(_context)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }
        public string GetIdByMember
        {
            get { return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "id").Value; }
        }

        public IQueryable<MessageView> GetAllInView()
        {
            return _db.MessageView.OrderByDescending(p => p.CreateDate);
        }
    }
}
