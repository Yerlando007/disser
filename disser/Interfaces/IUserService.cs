using disser.Models.Base;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;

namespace disser.Interfaces
{
    public interface IUserService
    {
        Task<LoginRole<List<AuthOptions>>> GetIdentity([FromForm] LoginFormData user);
        Task<List<User>> Register([FromForm] UserFormData user);
        Task<List<User>> GetUsers();
        Task<List<User>> GetRukovoditel();
        Task<List<User>> GetIspoltinel(int id);
        Task<List<User>> VerifyUser([FromForm] VerifyFormData user);
    }
}
