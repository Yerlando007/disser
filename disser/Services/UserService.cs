using Microsoft.EntityFrameworkCore;
using disser.Interfaces;
using disser.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.Mvc;
using disser.Models.EF.Users;
using Newtonsoft.Json;

namespace disser.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
            => _db = db;
        public DateTime deletedotfromdate(DateTime datewithoutdot)
        {
            var newdatewithoutdot = datewithoutdot.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime newdatenormal = Convert.ToDateTime(newdatewithoutdot);
            return newdatenormal;
        }
        public List<string> _AddFiles(List<IFormFile> addedFiles)
        {
            List<string> filelist = new List<string>();
            if (addedFiles.Count > 0)
            {

                foreach (var file in addedFiles)
                {
                    string filename = file.FileName;
                    filename = Path.GetFileName(filename);
                    string extension = Path.GetExtension(filename);
                    string namefile = Path.GetFileNameWithoutExtension(filename);
                    string[] time = DateTime.Now.TimeOfDay.ToString().Split('.');
                    string newtime = time[0].Replace(":", "-");
                    string date = DateTime.Now.Date.ToString("yyyy-MM-dd") + "-" + newtime + "-";
                    string newFileName = date + namefile + extension;
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//UserFiles", newFileName);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    file.CopyToAsync(stream);
                    filelist.Add(newFileName);
                }
            }
            return filelist;
        }
        public string _UserPhoto(IFormFile addedFiles)
        {
            string filename = addedFiles.FileName;
            filename = Path.GetFileName(filename);
            string extension = Path.GetExtension(filename);
            string namefile = Path.GetFileNameWithoutExtension(filename);
            string[] time = DateTime.Now.TimeOfDay.ToString().Split('.');
            string newtime = time[0].Replace(":", "-");
            string date = DateTime.Now.Date.ToString("yyyy-MM-dd") + "-" + newtime + "-";
            string newFileName = date + namefile + extension;
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//UserFiles", newFileName);
            var stream = new FileStream(uploadpath, FileMode.Create);
            addedFiles.CopyToAsync(stream);
            return newFileName;
        }
        public async Task<List<User>> Register([FromForm] FormDataUser user)
        {
            List<User> result = new List<User>();
            List<Documents> docs = new List<Documents>();   
            string userPhoto = "";
            List<string> docName = new List<string>();
            if (user.UserPhoto != null)
            {
                userPhoto = _UserPhoto(user.UserPhoto);
            }
            if (user.DocumentName != null)
            {
                docName = _AddFiles(user.DocumentName);
            }
            for (int i = 0; i < docName.Count; i++)
            {
                docs.Add(new Documents
                {
                    DocumentName = docName[i],
                    DocumentType = user.DocumentType[i]
                });
            }
            var newUser = new User()
            {
                FIO = user.FIO,
                Username = user.Username,
                Password = user.Password,
                Mail = user.Mail,
                BirthDay = deletedotfromdate(user.BirthDay),
                Role = "Пользователь",
                UserPhoto = userPhoto != null ? userPhoto : null,
                isVerify = false,
                LeaderID= user.LeaderID != null ? user.LeaderID : null,
                Documents = docs != null ? docs : null
            };
            await _db.Users.AddAsync(newUser);
            result.Add(newUser);
            await _db.SaveChangesAsync();
            return result;
        }
    }
}
