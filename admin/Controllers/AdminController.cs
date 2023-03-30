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
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _uow;
        public AdminController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            return View(_uow.Admin.GetAll());
        }
        [HttpGet]
        public IActionResult Manage(int? id)
        {
            if (id != null)
            {
                var Admin = _uow.Admin.GetById(Convert.ToInt32(id));
                return View(Admin);
            }
            else
            {
                return View(new Admin());
            }
        }
        [HttpPost]
        public IActionResult Manage(Admin entity)
        {
            entity.LastLoginDate = DateTime.Now;
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    if (entity.Image == null)
                    {
                        entity.Image = "default-profile.png";
                    }
                    _uow.Admin.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity); 
            }
            else
            {
                if (ModelState.IsValid)
                {
                    entity.RegisterDate = DateTime.Now;
                    _uow.Admin.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Admin = _uow.Admin.GetById(Convert.ToInt32(id));
            return View(Admin);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Admin = _uow.Admin.GetById(Convert.ToInt32(id));
            if (Admin != null)
            {
                _uow.Admin.Delete(Admin);
            }
            return View(Admin);
        }
        [HttpPost]
        public JsonResult Root(int Id)
        {
            _uow.Admin.IsAdmin(Id);
            return Json("ok");
        }
    }
}
