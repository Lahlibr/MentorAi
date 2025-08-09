using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class RecentProblemDto
    {
        public int ProblemId { get; set; }
        public string Title { get; set; }
        public bool IsSolved { get; set; }
        public DateTime LastAttempted { get; set; }
    }
}
