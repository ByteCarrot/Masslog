
namespace ByteCarrot.Masslog.Core.Infrastructure
{
    public class Result
    {
        public Result()
        {
            Success = true;
            Error = null;
        }

        public Result(string error)
        {
            Success = false;
            Error = error;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }
    }
}
