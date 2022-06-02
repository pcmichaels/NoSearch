namespace NoSearch.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }        
        public T Data { get; private set; }
        public string[] Errors { get; private set; }

        public static Result<T> Success(T data) =>
            new Result<T>()
            {
                Data = data,
                IsSuccess = true
            };

        public static Result<T> Fail(string error) =>
            new Result<T>()
            {
                IsSuccess = false,
                Errors = new[] {error}
            };
    }
}