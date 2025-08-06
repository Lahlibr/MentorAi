using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class ProblemPerformanceDto
    {
        public int ProblemId { get; set; }
        public string ProblemTitle { get; set; }
        public string Status { get; set; } // Solved, Attempted, Not Attempted
        public int Attempts { get; set; }
        public double TimeSpent { get; set; } // in minutes
        public int WrongSubmissions { get; set; }
        public DateTime? FirstAttemptTime { get; set; }
        public DateTime? SolvedTime { get; set; }
        public int SuccessfulAttempts { get; set; }
        public double AverageTimeToSolve { get; set; }
        public double SuccessRate { get; set; }

        public List<Submission> Submissions { get; set; }
    }
}
