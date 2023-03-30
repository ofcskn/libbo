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
    public class MulctController : Controller
    {
        private readonly IUnitOfWork _uow;
        public MulctController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            return View(_uow.Mulct.GetAllInView());
        }
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Manage(Mulct entity)
        {
            return View();
        }
        public IActionResult Lend()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var mulct = _uow.Mulct.GetById(Convert.ToInt32(id));
            return View(mulct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var mulct = _uow.Mulct.GetById(Convert.ToInt32(Id));
            if (mulct != null)
            {
                _uow.Mulct.Delete(mulct);
                return RedirectToAction("List");
            }
            return View(mulct);
        }
    }
}
