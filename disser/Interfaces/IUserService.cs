using disser.Models.EF;

namespace disser.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Register(string userName, string newUserName, string FIO, string? filial);
    }
}
