using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;

namespace disser.Interfaces
{
    public interface IGOST
    {
        Task<List<CreatedGOST>> CreateGOST([FromForm] GostFormData gost);
        Task<CreatedGOST> GetCreatedGOST(int id, string userName);
        Task<CreatedGOST> TakeGOSTs([FromForm] TakeGOSTs gost, string userName);
        Task<List<AllGOST>> AddGOSTS();
        Task<CreatedGOST> ChoiseRukovoditel([FromForm] ChoisedRukovoditel choisedRukovoditel);
    }
}
