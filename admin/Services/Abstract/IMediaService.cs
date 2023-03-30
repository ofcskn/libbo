using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Services.Abstract
{
    public interface IMediaService
    {
        public string FileUploadSame();
        public bool MediaExits(string FileName);
        public void InsertMedia(Media Media);
        public void DeleteMedia(string fileName);
        public void DeleteMediaById(int id);
        public string FilePath(string fileName);
        public string FilePathAdmin(string fileName);
        public string FileUploadDirectory();
        public IQueryable<Media> GetAll();
        public string GetFileNameById(int id);
    }
}
