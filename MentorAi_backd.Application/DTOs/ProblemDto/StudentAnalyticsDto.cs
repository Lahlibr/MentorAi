using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class StudentAnalyticsDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ProblemsSolved { get; set; }
        public int TotalAttempts { get; set; }
        public double AverageTimeToSolve { get; set; }
        public double SuccessRate { get; set; }
        public List<ProblemPerformanceDto> ProblemPerformances { get; set; } = new();
    }
}
