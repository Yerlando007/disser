using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;

namespace disser.Interfaces
{
    public interface IGOST
    {
        Task<List<GOST>> CreateGOST(string userName, [FromForm] GostFormData gost);
    }
}
