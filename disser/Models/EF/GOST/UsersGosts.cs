using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class UsersGosts : EntityBase<int>
    {
        public int gostID { get; set;}
        public string gostName { get; set;}
    }
}
