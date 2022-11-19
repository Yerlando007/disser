using disser.Models.EF.Users;

namespace disser.Models.Base
{
    public class LoginRole<T>
    {
        public T LoginInfo { get; set; }
        public string Role { get; set; }
    }
}
