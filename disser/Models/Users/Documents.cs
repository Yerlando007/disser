using disser.Models.Base;

namespace disser.Models.Users
{
    public class Documents : EntityBase<int>
    {
        public string DocumentName { get; set; }
        public int UserId { get; set; }
    }
}
