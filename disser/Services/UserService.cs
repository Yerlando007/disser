using Microsoft.EntityFrameworkCore;
using disser.Interfaces;
using disser.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.AspNetCore.Mvc;
using disser.Models.EF.Users;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Xml.Linq;

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
        private ClaimsIdentity _GetIdentity(string username, string password)
        {
            var person = _db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
        public async Task<List<User>> GetUserInfo(int id)
        {
            List<User> user = new List<User>();
            var users = await _db.Users.FirstOrDefaultAsync(r => r.Id == id);
            var document = await _db.Documents.Where(r => r.UserId == id).ToListAsync();
            users.Documents = document;
            user.Add(users);
            return user;
        }
        public async Task<List<User>> VerifyUser([FromForm] Verify verify)
        {
            List<User> result = new List<User>();
            var request = await _db.Users.FirstOrDefaultAsync(r => r.Id == verify.Id);
            request.isVerify = true;
            request.LeaderID = verify.LeaderID;
            result.Add(request);
            await _db.SaveChangesAsync();
            return result;
        }
        public async Task<List<User>> GetRukovoditel()
        {
            var users = await _db.Users.Where(r => r.Role == "Руководитель").ToListAsync();
            return users;
        }
        public async Task<List<User>> GetIspoltinel(int id)
        {
            var users = await _db.Users.Where(r => r.Role == "Исполнитель" && r.LeaderID == id).ToListAsync();
            return users;
        }
        public async Task<List<User>> GetUsers()
        {
            var users = await _db.Users.ToListAsync();
            return users;
        }
        public async Task<LoginRole<List<AuthOptions>>> GetIdentity([FromForm] Login user)
        {
            LoginRole<List<AuthOptions>> role = new LoginRole<List<AuthOptions>>();
            var encodedPassword = User.Encode(user.Password);
            var person = _db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == encodedPassword);
            List<AuthOptions> result = new List<AuthOptions>();
            var identity = _GetIdentity(user.Username, encodedPassword);
            if (identity == null)
            {
                return null;
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new AuthOptions
            {
                access_token = encodedJwt,
                username = identity.Name
            };         
            result.Add(response);
            role.LoginInfo = result;
            role.Role = person.Role;
            return role;
        }
        public async Task<List<User>> Register([FromForm] FormDataUser user)
        {
            List<User> result = new List<User>();
            List<Documents> docs = new List<Documents>();   
            string userPhoto = "";
            List<string> docName = new List<string>();
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
                Password = User.Encode(user.Password),
                Mail = user.Mail,
                Role = "Пользователь",
                isVerify = false,
                Documents = docs != null ? docs : null,
                Comments = user.Commemts != null ? user.Commemts : null,
            };
            await _db.Users.AddAsync(newUser);
            result.Add(newUser);
            await _db.SaveChangesAsync();
            return result;
        }
    }
}
