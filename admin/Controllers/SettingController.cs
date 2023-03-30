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
    public class SettingController : Controller
    {
        private readonly IUnitOfWork _uow;
        public SettingController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manage()
        {
            SettingViewModel viewModel = new SettingViewModel();
            if (_uow.Admin.GetTypeByAdmin == "admin")
            {
                viewModel.Admin = _uow.Admin.GetById(Convert.ToInt32(_uow.Admin.GetIdByAdmin));
            }
            else if (_uow.Member.GetTypeByMember == "member")
            {
                viewModel.Member = _uow.Member.GetById(Convert.ToInt32(_uow.Member.GetIdByMember));
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Manage(SettingViewModel viewModel)
        {
            if (viewModel.Admin != null)
            {
                if (ModelState.IsValid)
                {
                    _uow.Admin.Update(viewModel.Admin);
                    return RedirectToAction("Index");
                }
                return View(viewModel.Admin);
            }
            else if (viewModel.Member != null)
            {
                if (ModelState.IsValid)
                {
                    var phone = viewModel.Member.Phone;
                    viewModel.Member.Phone = phone.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    _uow.Member.Update(viewModel.Member);
                    return RedirectToAction("Index");
                }
                return View(viewModel.Member);
            }
            else
            {
                return View();
            }
        }
    }
}
