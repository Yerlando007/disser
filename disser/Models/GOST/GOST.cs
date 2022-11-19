using disser.Models.Base;

namespace disser.Models.GOST
{
    public class GOST : EntityBase<int>
    {
        public int userId { get; set; }
        public List<RukovoditelWantWork>? RukovoditelWantWork { get; set; }
        public int ChoisedRukovoditelID { get; set; }
        public bool OnWork { get; set; }
        public List<SimilarGOST> similarGOSTs { get; set; } 
        public bool isFinished { get; set; }
    }
}
