using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Enums
{
    public enum SubmissionStatus
    {
        Pending,
        Accepted,
        WrongAnswer,
        RuntimeError,
        TimeLimitExceeded,
        CompilationError,
        InternalError
    }
}
