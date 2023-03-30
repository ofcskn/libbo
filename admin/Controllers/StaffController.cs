using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class StaffController : Controller
    {
        private readonly IUnitOfWork _uow;
        public StaffController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            return View(_uow.Staff.GetAll());
        }
        [HttpGet]
        public IActionResult Manage(int? id)
        {
            if (id != null)
            {
                var Staff = _uow.Staff.GetById(Convert.ToInt32(id));
                return View(Staff);
            }
            else
            {
                return View(new Staff());
            }
        }
        [HttpPost]
        public IActionResult Manage(Staff entity)
        {
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Staff.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Staff.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Staff = _uow.Staff.GetById(Convert.ToInt32(id));
            return View(Staff);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Staff = _uow.Staff.GetById(Convert.ToInt32(id));
            if (Staff != null)
            {
                _uow.Staff.Delete(Staff);
            }
            return View(Staff);
        }
    }
}
