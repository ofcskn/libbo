using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ContactController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            return View(_uow.Contact.GetAll());
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            return View(_uow.Contact.GetById(id));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _uow.Contact.GetById(Convert.ToInt32(id));
            return View(contact);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _uow.Contact.GetById(Convert.ToInt32(id));
            if (contact != null)
            {
                _uow.Contact.Delete(contact);
                return RedirectToAction("List");
            }
            return View(contact);
        }
    }
}
