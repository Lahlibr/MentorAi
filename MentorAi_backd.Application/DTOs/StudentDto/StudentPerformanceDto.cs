using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class StudentPerformanceDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ProblemId { get; set; }
        public string ProblemTitle { get; set; }

        public int TotalAttempts { get; set; }
        public DateTime FirstAttemptTime { get; set; }
        public DateTime? FirstCorrectTime { get; set; }
        public DateTime LastAttemptTime { get; set; }
        public bool IsSolved { get; set; }
    }
}
