using disser.Models.Base;
using disser.Models.Users;

namespace disser.Models.EF
{
    public class User : EntityBase<int>
    {
        public string FIO { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDay { get; set; }
        public string Role { get; set; }
        public IFormFile? UserPhoto { get; set; } 
        public bool isVerify { get; set; }
        public Teams? LeaderID { get; set; }
        public Documents? Documents { get; set; }  
        public static string Encode(string password)
        {
            try
            {
                byte[] EncDateByte = new byte[password.Length];
                EncDateByte = System.Text.Encoding.UTF8.GetBytes(password);
                string EncryptedData = Convert.ToBase64String(EncDateByte);
                return EncryptedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Encode: " + ex.Message);
            }
        }
    }
}
