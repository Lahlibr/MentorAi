using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Enums
{
    public enum ReviewerStatus
    {
        Pending,    // Default status upon registration
        Approved,   // Admin has approved the reviewer
        Rejected,   // Admin has rejected the application
        Suspended
    }
}
