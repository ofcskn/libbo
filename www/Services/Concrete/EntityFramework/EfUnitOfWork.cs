using www.Services.Abstract;
using Entities.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace www.Services.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _db;
        private IAboutService _aboutService;
        private ICategoryService _categoryService;
        private IContactService _contactService;
        private IBookService _bookService;
        private IAuthorService _authorService;
        private IStaffService _staffService;
        private IMemberService _memberService;
        
        public EfUnitOfWork(DatabaseContext db)
        {
            _db = db ?? throw new ArgumentNullException("_db can not be null");
        }

        public IAboutService About
        {
            get
            {
                return _aboutService ?? (_aboutService = new EfAboutService(_db));
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

        public IMemberService Member
        {
            get
            {
                return _memberService ?? (_memberService = new EfMemberService(_db));
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        } 
    }
}
