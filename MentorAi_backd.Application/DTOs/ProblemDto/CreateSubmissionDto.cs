using MentorAi_backd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class CreateSubmissionDto
    {
        public int StudentId { get; set; }
        public int ProblemId { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
    }
}
