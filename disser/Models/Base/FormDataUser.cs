using disser.Models.EF.Users;

namespace disser.Models.Base
{
    public class FormDataUser
    {
        public string FIO { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Role { get; set; }
        public IFormFile? UserPhoto { get; set; }
        public bool? isVerify { get; set; }
        public int? LeaderID { get; set; }
        public List<IFormFile>? DocumentName { get; set; }
        public List<string>? DocumentType { get; set; }
    }
}
