namespace NoSearch.Common
{
    public class DataResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string[]? Errors { get; private set; }
        public string FirstError
        {
            get
            {
                if (Errors != null && Errors.Any())
                    return Errors.First();
                return string.Empty;
            }
        }

        public static DataResult<T> Success(T data) =>
            new DataResult<T>()
            {
                Data = data,
                IsSuccess = true
            };

        public static DataResult<T> Fail(string error) =>
            new DataResult<T>()
            {
                IsSuccess = false,
                Errors = new[] { error }
            };

    }
}