using disser.Models.Base;

namespace disser.Models.EF.Users
{
    public class Documents : EntityBase<int>
    {
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
    }
}
