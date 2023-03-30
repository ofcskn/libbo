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
    public class EfThreadService : EfGenericService<Thread>, IThreadService
    {
        IHttpContextAccessor _httpContextAccessor;
        public EfThreadService(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) : base(_context)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DatabaseContext _db
        {
            get { return _context as DatabaseContext; }
        }

        public ThreadView GetByIdInView(int id)
        {
            return _db.ThreadView.FirstOrDefault(p=>p.Id == id);
        }

        public ThreadView GetThreadByFriend(string memberId, int friendId)
        {
            return _db.ThreadView.FirstOrDefault(p => p.CreateMemberId.ToString() == memberId && p.ReceiverMemberId == friendId);
        }

        public IQueryable<ThreadView> GetThreadInView()
        {
            return _db.ThreadView;
        }
    }
}
