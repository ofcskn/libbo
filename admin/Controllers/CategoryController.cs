using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using admin.Models;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            return View(await PaginatedList<Category>.CreateAsync(_uow.Category.GetAll(), pageNumber, 10));
        }
        [HttpGet]
        public IActionResult Manage(int? id)
        {
            if (id != null)
            {
                var category = _uow.Category.GetById(Convert.ToInt32(id));
                return View(category);
            }
            else
            {
                return View(new Category());
            }
        }
        [HttpPost]
        public IActionResult Manage(Category entity)
        {
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Category.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Category.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _uow.Category.GetById(Convert.ToInt32(id));
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _uow.Category.GetById(Convert.ToInt32(id));
            if (category != null)
            {
                _uow.Category.Delete(category);
                return RedirectToAction("List");
            }
            return View(category);
        }
    }
}
