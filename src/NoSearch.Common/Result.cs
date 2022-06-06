namespace NoSearch.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }                
        public string[] Errors { get; private set; }

        public static Result Success() =>
            new Result()
            {                
                IsSuccess = true
            };

        public static Result Fail(string error) =>
            new Result()
            {
                IsSuccess = false,
                Errors = new[] {error}
            };
    }
}