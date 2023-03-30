using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using admin.Services.Abstract;
using admin.Services.Concrete.EntityFramework;
using admin.Utilities;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class MediaController : Controller
    {
        private readonly IUnitOfWork _uow;
        public MediaController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult UploadFiles()
        {
            return RedirectToAction("Manage");
        }
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Manage(IFormFile avatar)
        {
            Media media = new Media();
            media.Date = DateTime.Now;
            media.FileNames = avatar.FileName;
            string fileName = Helpers.ToSlug(Path.GetFileNameWithoutExtension(media.FileNames));
            string extension = Path.GetExtension(media.FileNames);
            string fullFileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string filePath = Path.Combine(_uow.Media.FileUploadDirectory(), fullFileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }
            media.FileNames = fullFileName;
            _uow.Media.InsertMedia(media);
            return Json("refresh");
        }

        [HttpPost]
        public JsonResult DeleteImage(string fileName)
        {
            try
            {
                _uow.Media.DeleteMedia(fileName);
                //Listeye döndüğünde sayfa güncellemesi olması gerekiyor. Olmadığı için silindiği görünmüyor. Nasıl yaparım?
            }
            catch { }

            try
            {
                string fullPath = _uow.Media.FilePath(fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch { }

            try
            {
                string fullPath = _uow.Media.FilePath(fileName) + Path.GetFileNameWithoutExtension(fileName) + ".webp";
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch { }

            return Json("refresh");
        }
    }
}
