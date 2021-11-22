namespace back_end.Helpers
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Data = value };
        public static Result<T> Failure(string message) => new Result<T> { IsSuccess = false, Message = message };
    }
}