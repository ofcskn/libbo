using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Services.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        //Servisleri tek bir elden yönetmemizi sağlayan bir merkezdir UNitOfWork
        IAboutService About { get; }
        ICategoryService Category { get; }
        IContactService Contact { get; }
        IAuthorService Author { get; }
        IBookService Book { get; }
        IStaffService Staff { get; }
        IMemberService Member { get; }
    }
}
