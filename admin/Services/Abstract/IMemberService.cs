using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IMemberService:IGenericService<Member>
    {
        Member GetMember(string username, string password);
        string GetNameByMember { get; }
        string GetImageByMember { get; }
        string GetTypeByMember { get; }
        string GetIdByMember { get; }
        string GetMailByMember { get; }
    }
}
