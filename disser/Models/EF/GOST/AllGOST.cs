using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class AllGOST : EntityBase<int>
    {
        public string GOSTName { get; set; }
        public string KeyWords { get; set; }
        public int? CreatedGOSTId { get; set; }
    }
}
