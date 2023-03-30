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
    [Authorize(Policy = "Admin")]
    public class MemberController : Controller
    {
        private readonly IUnitOfWork _uow;
        public MemberController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            var filtered = _uow.Member.GetAll();//Maybe I can filter for members.
            return View(filtered);
        }
        [HttpGet]
        public IActionResult Manage(int id)
        {
            return View(_uow.Member.GetById(id));
        }
        [HttpPost]
        public IActionResult Manage(Member entity)
        {
            if (entity.Id != 0)
            {
                if (ModelState.IsValid)
                {
                    if (entity.Image == null)
                    {
                        entity.Image = "default-profile.png";
                    }
                    _uow.Member.Update(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _uow.Member.Add(entity);
                    return RedirectToAction("List");
                }
                return View(entity);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Member = _uow.Member.GetById(Convert.ToInt32(id));
            return View(Member);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var Member = _uow.Member.GetById(Convert.ToInt32(Id));
            if (Member != null)
            {
                _uow.Member.Delete(Member);
                return RedirectToAction("List");
            }
            return View(Member);
        }
    }
}
