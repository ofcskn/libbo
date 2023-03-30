using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IAdminService:IGenericService<Admin>
    {
        Admin IsAdmin(int id);
        Admin GetAdmin(string username, string password);
        string GetNameByAdmin { get; }
        string GetImageByAdmin { get; }
        string GetTypeByAdmin { get; }
        string GetIdByAdmin { get; }
        string GetMailByAdmin { get; }
    }
}
