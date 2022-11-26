using disser.Models.Base;

namespace disser.Models.EF.GOST
{
    public class GOSTKeyWords : EntityBase<int>
    {
        public string KeyWords { get; set; }
        public int GOSTID { get; set; }
    }
}
