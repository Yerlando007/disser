using disser.Models.Base;

namespace disser.Models.EF.Users
{
    public class User : EntityBase<int>
    {
        public string FIO { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDay { get; set; }
        public string Role { get; set; }
        public string? UserPhoto { get; set; }
        public bool isVerify { get; set; }
        public int? LeaderID { get; set; }
        public List<Documents>? Documents { get; set; }
        public string? Comments { get; set; } 
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
