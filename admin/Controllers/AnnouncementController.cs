using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AnnouncementController(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult List()
        {
            return View(_uow.Announcement.GetAll());
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult Manage(int? id)
        {

            if (id != null)
            {
                var announcement = _uow.Announcement.GetById(Convert.ToInt32(id));
                return View(announcement);
            }
            else
            {
                return View(new Announcement());
            }
        }
        [HttpPost]
        public IActionResult Manage(Announcement entity)
        {
            entity.Date = DateTime.Now;
            entity.Announcer = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "role").Value;
            entity.Enabled = true;
            
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    _uow.Announcement.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Announcement.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            return View(_uow.Announcement.GetById(id));
        }
        [Authorize(Policy = "Admin")]
        public JsonResult Delete(int id)
        {
            var announcement = _uow.Announcement.GetById(Convert.ToInt32(id));
            if (announcement != null)
            {
                _uow.Announcement.Delete(announcement);
                return Json("yes");
            }
            return Json("no");
        }
        public JsonResult ReadCount(int id)
        {
            var announcement = _uow.Announcement.GetById(Convert.ToInt32(id));
            int readNumber = Convert.ToInt32(announcement.ReadNumber);
            if (announcement != null)
            {
                announcement.ReadNumber = readNumber + 1;
                _uow.Announcement.Update(announcement);
                return Json("yes");
            }
            return Json(id);
        }
    }
}
