using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Common.Exceptions
{
    public class CompilationException :Exception
    {
        public CompilationException(string message) : base(message)
        {
        }
        public CompilationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
