namespace MentorAi_backd.Exceptions
{
    public class ValidationException : Exception
    {
        // if full error
        public List<string> Errors { get; }
        public ValidationException(string message,List<string>? errors = null) : base(message)
        {
            Errors = errors ?? new List<string>();
        }
        //for specific errors
        public ValidationException(string message) : base(message) {
            Errors = new List<string> { };
        }
    }
}
