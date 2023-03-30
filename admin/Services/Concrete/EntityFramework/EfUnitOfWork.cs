using admin.Services.Abstract;
using Entities.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IAdminService _adminService;
        private IAnnouncementService _announcementService;
        private ICategoryService _categoryService;
        private IContactService _contactService;
        private IBookService _bookService;
        private IMediaService _mediaService;
        private IProcessService _processService;
        private IMulctService _mulctService;
        private IMessageService _messageService;
        private IAuthorService _authorService;
        private IStaffService _staffService;
        private IMemberService _memberService;
        private IMailMessageService _mailMessageService;
        private IThreadService _threadService;
        private IToDoListService _toDoListService;
        
        public EfUnitOfWork(DatabaseContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _db = db ?? throw new ArgumentNullException("_db can not be null");
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        public IAdminService Admin
        {
            get
            {
                return _adminService ?? (_adminService =  new EfAdminService(_db, _httpContextAccessor));
            }
        }  
        public IAnnouncementService Announcement
        {
            get
            {
                return _announcementService ?? (_announcementService =  new EfAnnouncementService(_db));
            }
        }

        public ICategoryService Category
        {
            get
            {
                return _categoryService ?? (_categoryService = new EfCategoryService(_db));
            }
        } 
        public IContactService Contact
        {
            get
            {
                return _contactService ?? (_contactService = new EfContactService(_db));
            }
        }
        public IAuthorService Author
        {
            get
            {
                return _authorService ?? (_authorService = new EfAuthorService(_db));
            }
        }
        public IBookService Book
        {
            get
            {
                return _bookService ?? (_bookService = new EfBookService(_db));
            }
        }
        public IStaffService Staff
        {
            get
            {
                return _staffService ?? (_staffService = new EfStaffService(_db));
            }
        }

        public IMediaService Media
        {
            get
            {
                return _mediaService ?? (_mediaService = new EfMediaService(_db, _webHostEnvironment));
            }
        } 
        public IMessageService Message
        {
            get
            {
                return _messageService ?? (_messageService = new EfMessageService(_db, _httpContextAccessor));
            }
        } 
        public IMailMessageService MailMessage
        {
            get
            {
                return _mailMessageService ?? (_mailMessageService = new EfMailMessageService(_db, _httpContextAccessor));
            }
        }
        public IMulctService Mulct
        {
            get
            {
                return _mulctService ?? (_mulctService = new EFMulctService(_db));
            }
        }
        public IMemberService Member
        {
            get
            {
                return _memberService ?? (_memberService = new EfMemberService(_db,_httpContextAccessor));
            }
        }
        public IProcessService Process
        {
            get
            {
                return _processService ?? (_processService = new EfProcessService(_db));
            }
        } 
        public IThreadService Thread
        {
            get
            {
                return _threadService ?? (_threadService = new EfThreadService(_db, _httpContextAccessor));
            }
        }
        public IToDoListService ToDoList
        {
            get
            {
                return _toDoListService ?? (_toDoListService = new EfToDoListService(_db, _httpContextAccessor));
            }
        }
        public void Dispose()
        {
            _db.Dispose();
        } 
    }
}
