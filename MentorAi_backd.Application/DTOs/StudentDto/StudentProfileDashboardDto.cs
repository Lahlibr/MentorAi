using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.DTOs.ProblemDto;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class StudentProfileDashboardDto
    {
        public int ProblemsSolved { get; set; }
        public int ReviewsReceived { get; set; }
        public double AverageScore { get; set; }
        public double SkillGrowth { get; set; }
        public List<RecentProblemDto> RecentProblems { get; set; } = new();
    }
}
