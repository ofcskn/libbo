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
    public class BookController : Controller
    {
        private readonly IUnitOfWork _uow;
        public BookController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> List(string categoryFilter, string q, string scoreFilter, int pageNumber = 1)
        {
            var filtered = _uow.Book.GetAllInView();
            if (!string.IsNullOrEmpty(scoreFilter))
            {
                if (scoreFilter == "topBook")
                {
                    filtered = _uow.Book.GetByTopRead();
                }
                else if (scoreFilter == "scoreBook")
                {
                    filtered = _uow.Book.GetByTopScore();
                }
            }
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                filtered = filtered.Where(p => p.CategoryName.Contains(categoryFilter));
            }
            if (!string.IsNullOrEmpty(q))
            {
                filtered = filtered.Where(p => p.Name.Contains(q) || p.PrintingYear.Contains(q) || p.AuthorName.Contains(q));
            }
            return View(await PaginatedList<BookView>.CreateAsync(filtered, pageNumber, 10));
        }
        public IActionResult History()
        {
            string Id = _uow.Member.GetIdByMember;
            var filter = _uow.Process.GetAll().Where(p => p.MemberId == Convert.ToInt32(Id));

            return View(filter);
        }
        [Authorize(Policy = "Admin")]

        [HttpGet]
        public IActionResult Manage(int? Id)
        {
            if (Id != null)
            {
                return View(_uow.Book.GetById(Convert.ToInt32(Id)));
            }
            else
            {
                return View(new Book());
            }
        }
        [HttpPost]
        public IActionResult Manage(Book entity)
        {

            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Book.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Book.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [Authorize(Policy = "Admin")]

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _uow.Book.GetById(Convert.ToInt32(id));
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var book = _uow.Book.GetById(Convert.ToInt32(Id));
            if (book != null)
            {
                _uow.Book.Delete(book);
                return RedirectToAction("List");
            }
            return View(book);
        }
        [HttpPost]
        public JsonResult Score(int score, int id)
        {
            var book = _uow.Book.GetById(id);
            int oldScore = Convert.ToInt32(book.Score);
            var newScore = Convert.ToInt32(book.Score);
            book.Score = Convert.ToString((oldScore + score) / 5);
            _uow.Book.Update(book);
            var data = new
            {
                score,
                newScore
            };
            return Json(data);
        }
    }
}
