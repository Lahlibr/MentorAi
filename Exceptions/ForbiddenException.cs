namespace MentorAi_backd.Exceptions
{
    public class ForbiddenException :Exception
    {
        public ForbiddenException(string message = "Access to this resource is forbidden."): base(message) { }
    }
}
