using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IThreadService:IGenericService<Thread>
    {
        IQueryable<ThreadView> GetThreadInView();
        ThreadView GetByIdInView(int id);
        ThreadView GetThreadByFriend(string memberId, int friendId);
    }
}
