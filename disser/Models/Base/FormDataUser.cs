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
        public int Id { get; set; }
        public bool IsVerify { get; set; }
        public string Role { get; set; }
        public int? LeaderID { get; set; }
    }
    public class GostFormData
    {
        public List<IFormFile> Gost { get; set; }
    }
    public class RukovoditelWantWorkFormData
    {
        public List<string> DocumentName { get; set; }
        public List<DateTime> StartDate { get; set; }
        public List<DateTime> EndDate { get; set; }
    }
}
