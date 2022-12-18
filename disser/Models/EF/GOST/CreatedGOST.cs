using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class CreatedGOST : EntityBase<int>
    {
        public int userId { get; set; }
        public List<AllGOST> CreatedGOSTAddedFiles { get; set; }
        public List<RukovoditelWantWork>? RukovoditelWantWork { get; set; }
        public int? ChoisedRukovoditelID { get; set; }
        public bool OnWork { get; set; }
        public List<SimilarFile>? similarGOSTs { get; set; }
        public bool isFinished { get; set; }
    }
}
