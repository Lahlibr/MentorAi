using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class LeaderboardDto
    {
        public List<LeaderboardEntryDto> Entries { get; set; } = new();
        public int TotalStudents { get; set; }
    }
}
