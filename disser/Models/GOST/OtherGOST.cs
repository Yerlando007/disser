using disser.Models.Base;

namespace disser.Models.GOST
{
    public class OtherGOST : EntityBase<int>
    {
        public string GOSTName { get; set; }
        public List<GOSTKeyWords> KeyWords { get; set;}
    }
}
