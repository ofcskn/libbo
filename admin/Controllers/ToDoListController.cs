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
    public class ToDoListController : Controller
    {
        private readonly IUnitOfWork _uow;
        public ToDoListController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            return View(await PaginatedList<ToDoList>.CreateAsync(_uow.ToDoList.GetAll().Where(p=>p.Enabled == false), pageNumber, 10));
        }
        [HttpPost]
        public JsonResult Check(int id)
        {
            var goal = _uow.ToDoList.GetById(id);
            if (goal.Enabled == false)
            {
                goal.Enabled = true;
            }
            else
            {
                goal.Enabled = false;
            }
            _uow.ToDoList.Update(goal);
            return Json(goal);
        }
        [HttpPost]
        public JsonResult Add(string todo)
        {
            if (todo != null)
            {
                ToDoList entity = new ToDoList
                {
                    Goal = todo,
                    Date = DateTime.Now,
                    Enabled = false,
                    PersonId = Convert.ToInt32(_uow.Admin.GetIdByAdmin),
                    Role = _uow.Admin.GetTypeByAdmin,
                };
                _uow.ToDoList.Add(entity);
                return Json(entity);
            }
            return Json("no");
        }
        [HttpPost]
        public JsonResult Update(int id, string goal)
        {
            var todo = _uow.ToDoList.GetById(id);
            todo.Goal = goal;
            _uow.ToDoList.Update(todo);
            return Json(todo);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var goal = _uow.ToDoList.GetById(id);
            if (goal != null)
            {
                _uow.ToDoList.Delete(goal);
                return Json(goal);
            }
            return Json("no");
        }
    }
}
