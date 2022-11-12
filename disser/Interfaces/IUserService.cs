using disser.Models.Base;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;

namespace disser.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Register([FromForm] FormDataUser user);
    }
}
