namespace Models
{
    public class ResponseStatus<T>
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }
    }
}
