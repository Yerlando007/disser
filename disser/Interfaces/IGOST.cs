using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;

namespace disser.Interfaces
{
    public interface IGOST
    {
        Task<List<CreatedGOST>> CreateGOST([FromForm] GostFormData gost, string userName);
        Task<CreatedGOST> GetCreatedGOST(int id);
        Task<RukovoditelWantWork> GiveTasktoIspolnitel(GiveTasktoIspolnitel giveTasktoIspolnitel);
        Task<List<RukovoditelWantWork>> TakeGOSTsByRukovoditel([FromForm] TakeGOSTs gost, string userName);
        Task<List<AllGOST>> AddGOSTS();
        Task<CreatedGOST> ChoiseRukovoditel([FromForm] ChoisedRukovoditel choisedRukovoditel);
        Task<CreatedGOST> RukovoditelAcceptWork([FromForm] RukovoditelAcceptWork rukovoditelAcceptWork);
        Task<List<TranslateFile>> TranslatorAddFile([FromForm] TranslatorAddFile translatorAddFile, string userName);
        Task<TranslateFile> AccepTranslatorWork([FromForm] AcceptTanslatorWork acceptTanslatorWork);
        Task<RukovoditelWantWork> IspolnitelWork([FromForm] IspolnitelWork ispolnitelWork, string userName);
    }
}
