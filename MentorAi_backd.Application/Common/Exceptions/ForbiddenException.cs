namespace MentorAi_backd.Application.Common.Exceptions
{
    public class ForbiddenException :Exception
    {
        public ForbiddenException(string message = "Access to this resource is forbidden."): base(message) { }
    }
}
