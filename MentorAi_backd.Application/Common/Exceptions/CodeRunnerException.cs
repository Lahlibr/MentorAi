using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Common.Exceptions
{
    public class CodeRunnerException : Exception
    {
        public CodeRunnerException(string message) : base(message)
        {

        }
        
        
    }
}
