using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class RukovoditelWantWork : EntityBase<int>
    {
        public string Comment { get; set; }
        public string? CommentFromIspolnitel { get; set; }
        public string? CommentToIspolnitel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string File { get; set; }
        public string? IspolnitelFile { get; set; }
        public bool isFinishedTask { get; set; }
        public int RukovoditelId { get; set; }
        public int? IspolnitelId { get; set; }
        public int CreatedGOSTId { get; set; }
    }
}
