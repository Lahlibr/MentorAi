using System.Runtime.Serialization;

namespace MentorAi_backd.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception inner) : base(message, inner) { }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
