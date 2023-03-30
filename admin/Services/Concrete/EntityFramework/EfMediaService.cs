using admin.Services.Abstract;
using admin.Utilities;
using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Concrete.EntityFramework
{
    public class EfMediaService : IMediaService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DatabaseContext _db;
        public EfMediaService(DatabaseContext db, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        public string FileUploadSame()
        {
            int lastIndexOf = _hostingEnvironment.ContentRootPath.LastIndexOf(_hostingEnvironment.ApplicationName);
            string path = _hostingEnvironment.WebRootPath;


            return path + @"\img\";
        }




        public bool MediaExits(string FileName)
        {
            return _db.Media.Any(p => p.FileNames == FileName);
        }

        public void InsertMedia(Media Media)
        {
            _db.Media.Add(Media);
            _db.SaveChanges();
        }

        public void DeleteMedia(string fileName)
        {
            _db.Media.Remove(_db.Media.FirstOrDefault(p => p.FileNames == fileName));
            _db.SaveChanges();
        }
        public void DeleteMediaById(int id)
        {
            _db.Media.Remove(_db.Media.FirstOrDefault(p => p.Id == id));
            _db.SaveChanges();
        }
        public string FilePath(string fileName)
        {
            string path = FileUploadDirectory();
            return path + Helpers.ChangeFileName(fileName);
        }
        public string FilePathAdmin(string fileName)
        {
            string path = FileUploadSame();
            return path + Helpers.ChangeFileName(fileName);
        }

        public string FileUploadDirectory()
        {
            int lastIndexOf = _hostingEnvironment.ContentRootPath.LastIndexOf(_hostingEnvironment.ApplicationName);
            string path = _hostingEnvironment.ContentRootPath.Substring(0, lastIndexOf);
            // string path = _hostingEnvironment.ContentRootPath.Replace(_hostingEnvironment.ApplicationName, "");
            if (Directory.Exists(path + @"\www"))
            {
                path = path + @"\www";
            }
            return path + @"\wwwroot\upload\";
        }
        public IQueryable<Media> GetAll()
        {
            return _db.Media.OrderByDescending(p => p.Date);
        }
        public string GetFileNameById(int id)
        {
            return _db.Media.FirstOrDefault(p => p.Id == id).FileNames;
        }
    }
}
