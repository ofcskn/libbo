using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Models;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _uow;
        public AuthorController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            return View(await PaginatedList<Author>.CreateAsync(_uow.Author.GetAll(), pageNumber, 10));
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult Manage(int? id)
        {
            if (id != null)
            {
                var Author = _uow.Author.GetById(Convert.ToInt32(id));
                return View(Author);
            }
            else
            {
                return View(new Author());
            }
        }
        [HttpPost]
        public IActionResult Manage(Author entity)
        {
            entity.Score = "4,3";
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Author.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Author.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Author = _uow.Author.GetById(Convert.ToInt32(id));
            return View(Author);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Author = _uow.Author.GetById(Convert.ToInt32(id));
            if (Author != null)
            {
                _uow.Author.Delete(Author);
                return RedirectToAction("List");
            }
            return View(Author);
        }
        [HttpGet]
        public IActionResult Book(int Id)
        {
            var book = _uow.Book.GetAll().Where(p=>p.AuthorId == Id);
            return View(book);
        }
    }
}
