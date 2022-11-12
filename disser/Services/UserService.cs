using Microsoft.EntityFrameworkCore;
using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

        public async Task<List<User>> Register(string userName, string newUserName, string FIO, string? filial)
        {
            string password = User.Encode("12345");
            var datewithoutdot = deletedotfromdate(DateTime.Now);
            List<User> result = new List<User>();
            var newUser = new User()
            {
                FIO= FIO,
                Username = newUserName,
                Password = password,
                Mail = FIO,
                BirthDay = DateTime.Now,
                Role = "КП",
                isVerify = true,
            };
            await _db.Users.AddAsync(newUser);
            result.Add(newUser);
            await _db.SaveChangesAsync();
            return result;
        }
    }
}
