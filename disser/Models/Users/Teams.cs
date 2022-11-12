using disser.Models.Base;

namespace disser.Models.Users
{
    public class Teams : EntityBase<int>
    {
        public int UserID { get; set; }
        public int LeaderId { get; set; }
    }
}
