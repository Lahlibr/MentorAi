using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class SubmissionStatusDto
    {
        public int SubmissionId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public SubmissionResultDto Result { get; set; }
    }
}
