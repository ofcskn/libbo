using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        //Servisleri tek bir elden yönetmemizi sağlayan bir merkezdir UNitOfWork
        IAdminService Admin { get; }
        IAnnouncementService Announcement { get; }
        IContactService Contact { get; }
        IMediaService Media { get; }
        IMulctService Mulct { get; }
        IProcessService Process { get; }
        ICategoryService Category { get; }
        IAuthorService Author { get; }
        IBookService Book { get; }
        IMessageService Message { get; }
        IMailMessageService MailMessage { get; }
        IStaffService Staff { get; }
        IMemberService Member { get; }
        IThreadService Thread { get; }
        IToDoListService ToDoList { get; }
    }
}
