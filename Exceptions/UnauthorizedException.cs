namespace MentorAi_backd.Exceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException (string message = "Authentication failed") : base(message) { }
    }
}
