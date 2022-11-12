namespace disser.Models.Base
{
    public class Response <T>
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
