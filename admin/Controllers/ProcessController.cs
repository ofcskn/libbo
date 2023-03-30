using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Models;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    public class ProcessController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ProcessController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            //return View(_uow.Process.GetAllByView().OrderByDescending(p=>p.ReturningDate));
            return View(await PaginatedList<ProcessView>.CreateAsync(_uow.Process.GetAllByView(), pageNumber, 8));
        }
        [HttpGet]
        public IActionResult Borrow(int? id)
        {
            var borrow = _uow.Process.GetById(Convert.ToInt32(id));
            if (borrow == null)
            {
                return View(new Process());
            }
            else
            {
                return View(borrow);
            }
        }
        [HttpPost]
        public IActionResult Borrow(Process entity)
        {
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Process.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    entity.Enabled = true;
                    _uow.Process.Add(entity);
                    var book = _uow.Book.GetById(entity.BookId);
                    book.Status = true;
                    _uow.Book.Update(book);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpPost]
        public JsonResult ReturnIt(int id)
        {
            if (id != 0)
            {
                var process = _uow.Process.GetById(id);
                process.ReturnItDate = DateTime.Now;
                process.Enabled = false;
                _uow.Process.Update(process);
                var book = _uow.Book.GetById(process.BookId);
                book.Status = false;
                _uow.Book.Update(book);
                
                return Json("yes");
            }
            return Json("no");
        }
        [HttpPost]
        public JsonResult MulctAmount(int id)
        {
            var process = _uow.Process.GetById(id);
            DateTime returningDate = DateTime.Parse(process.ReturningDate.ToShortDateString());
            DateTime dateOfToday = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan result = dateOfToday - returningDate;
            return Json(result.TotalDays);
        }
    }
}
