namespace Data.Shared.Models.Response
{
    public class ResponseMessage<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? MessageKey { get; set; }
        public T? Data { get; set; }
    }
}
