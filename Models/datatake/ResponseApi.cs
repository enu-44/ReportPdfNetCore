namespace pmacore_api.Models.datatake
{
    public class ResponseApi
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}