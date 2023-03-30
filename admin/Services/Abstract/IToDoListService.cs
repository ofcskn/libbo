using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IToDoListService : IGenericService<ToDoList>
    {
        string GetTypeByAdmin{ get;}
        string GetIdByAdmin { get; }
        IQueryable<ToDoList> GetAllByEnabled();
    }
}
