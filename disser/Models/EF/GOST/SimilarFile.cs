using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class SimilarFile : EntityBase<int>
    {
        public string GOSTName { get; set; }
        public string SimilarFiles { get; set; }
        public int CreatedGOSTId { get; set; }
    }
}
