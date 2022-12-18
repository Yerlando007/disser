using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class TranslateFile : EntityBase<int>
    {
        public double WorkPercentage { get; set; }
        public string TranslateFileName { get; set; }
        public string CommentFromTranslator { get; set; }
        public string? CommentToTranslator { get; set; }
        public int TranslatorId { get; set; }
        public bool IsFinished { get; set; }
        public int CreatedGOSTId { get; set; }
    }
}
