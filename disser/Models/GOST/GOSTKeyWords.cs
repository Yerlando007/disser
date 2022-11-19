using disser.Models.Base;

namespace disser.Models.GOST
{
    public class GOSTKeyWords : EntityBase<int>
    {
        public string key { get; set; }
        public int GOSTID { get; set; }
    }
}
