using disser.Models.EF.Users;

namespace disser.Models.Base
{
    public class UserFormData
    {
        public string FIO { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public List<IFormFile>? DocumentName { get; set; }
        public List<string>? DocumentType { get; set; }
        public string? Commemts { get; set; }
    }
    public class LoginFormData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class VerifyFormData
    {
        public int UserId { get; set; }
        public bool IsVerify { get; set; }
        public string Role { get; set; }
        public int? LeaderId { get; set; }
    }
    public class TakeGOSTs
    {
        public List<IFormFile> File { get; set; }
        public List<DateTime> StartDate  { get; set; }
        public List<DateTime> EndDate { get; set; }
        public List<string> Comment { get; set; }
        public int GOSTId { get; set; }
    }
    public class GostFormData
    {
        public List<IFormFile> Gost { get; set; }
    }
    public class GiveTasktoIspolnitel
    {
        public int IspoltitelID { get; set; }
        public int TaskId { get; set; }
    }
    public class IspolnitelWork
    {
        public IFormFile IspolnitelFile { get; set; }
        public string CommentFromIspolnitel { get; set; }
    }
    public class TranslatorAddFile
    {
        public IFormFile TranslateFileName { get; set; }
        public string? CommentFromTranslator { get; set; }

    }
    public class AcceptTanslatorWork
    {
        public bool IsFinished { get; set; }
        public string CommentToTranslator { get; set; }
        public int TranslatorId { get; set; }

    }
    public class RukovoditelAcceptWork
    {
        public bool IsFinished { get; set; }
        public string CommentToIspolnitel { get; set; }
        public int TaskId { get; set; }
    }
    public class ChoisedRukovoditel
    {
        public int RukovoditelId { get; set; }
        public int CreatedGOSTId { get; set; }
    }

    public class RukovoditelWantWorkFormData
    {
        public List<string> DocumentName { get; set; }
        public List<DateTime> StartDate { get; set; }
        public List<DateTime> EndDate { get; set; }
    }
}
