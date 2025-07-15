namespace MentorAi_backd.Application.Common.Exceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException (string message = "Authentication failed") : base(message) { }
    }
}
