using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class SimilarGOST : EntityBase<int>
    {
        public int CreatedGOSTID { get; set; }
        public int OtherGOSTID { get; set; }
    }
}
