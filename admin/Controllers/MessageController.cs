using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.Models;
using admin.Services.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace admin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _uow;
        public MessageController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [Authorize(Policy = "Admin")]
        public IActionResult Mail()
        {
            var filtered = _uow.MailMessage.GetAll();
            return View(filtered);
        }
        [HttpPost]
        public JsonResult ThreadControl(int threadId, int friendId)
        {
            var memberId = _uow.Member.GetIdByMember;
            var friendthread = _uow.Thread.GetByIdInView(threadId);
            var otherThread = _uow.Thread.GetThreadByFriend(memberId, friendId);

            var threadImage = friendthread.Image;

            var message = _uow.Message.GetAll().Where(p => p.ThreadId == otherThread.Id || p.ThreadId == friendthread.Id).OrderBy(p => p.CreateDate);

            var data = new
            {
                message,
                threadImage
            };
            return Json(data);
        }

        [HttpPost]
        public JsonResult SendMessage(int threadId, int memberId, string message)
        {
            Message message1 = new Message{
                CreateDate = DateTime.Now,
                IpAdress = "1:1",
                ReadDate = DateTime.Now,
                Text = message,
                MemberId = memberId,
                Seen = false,
                ThreadId = threadId
            };
            _uow.Message.Add(message1);
            var data = new
            {
                threadId,
                memberId,
                message
            };
            return Json(data);
        }
        [HttpPost]
        public JsonResult SearchMember(string searchText)
        {
            var member = _uow.Member.GetAll().Where(p=>p.Name.Contains(searchText));
            return Json(member);
        }
        public IActionResult Member()
        {
            MessageViewModel viewModel = new MessageViewModel();
            viewModel.MessageViews = _uow.Message.GetAllInView();
            viewModel.Threads = _uow.Thread.GetThreadInView();
            return View(viewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var book = _uow.Book.GetById(Convert.ToInt32(id));
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var book = _uow.Book.GetById(Convert.ToInt32(Id));
            if (book != null)
            {
                _uow.Book.Delete(book);
                return RedirectToAction("List");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult SendMail(MailMessage entity)
        {
            if (ModelState.IsValid)
            {
                entity.Date = DateTime.Now;
                entity.SenderMail = _uow.Admin.GetMailByAdmin;
                _uow.MailMessage.Add(entity);
                return RedirectToAction("mail");
            }
            return View(entity);
        }
    }
}
