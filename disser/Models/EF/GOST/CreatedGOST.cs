using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class CreatedGOST : EntityBase<int>
    {
        public int userId { get; set; }
        public List<AllGOST> AllGOST { get; set; }
        public List<RukovoditelWantWork>? RukovoditelWantWork { get; set; }
        public int? ChoisedRukovoditelID { get; set; }
        public bool OnWork { get; set; }
        public string? similarGOSTs { get; set; }
        public bool isFinished { get; set; }
    }
}
