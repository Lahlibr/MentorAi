using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class ProblemLanguageSolution
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string Language { get; set; }   
        public string SolutionTemplate { get; set; }
        public string ExpectedSolutionHash { get; set; }

        public virtual Problem Problem { get; set; }
    }
}
