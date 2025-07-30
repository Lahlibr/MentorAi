using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class LeaderboardEntryDto
    {
        public int Rank { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ProblemsSolved { get; set; }
        public double AverageTime { get; set; }
        public int TotalAttempts { get; set; }
        public double SuccessRate { get; set; }
    }
}
