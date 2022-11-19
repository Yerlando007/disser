namespace disser.Models.GOST
{
    public class RukovoditelWantWork
    {
        public string Comment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? IspoltitelID { get; set; }
        public IFormFile File { get; set; }
        public double? WorkPercentage { get; set; }
        public bool? isFinishedTask { get; set; } 
        public int userId { get; set; }
    }
}
