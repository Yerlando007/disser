using disser.Models.EF.Users;

namespace disser.Models.Base
{
    public class FormDataUser
    {
        public string FIO { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public List<IFormFile>? DocumentName { get; set; }
        public List<string>? DocumentType { get; set; }
        public string? Commemts { get; set; }
    }
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Verify
    {
        public int Id { get; set; }
        public bool IsVerify { get; set; }
        public int LeaderID { get; set; }
    }
}
