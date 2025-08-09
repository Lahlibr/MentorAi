using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class AttemptDetailDto
    {
        public int AttemptId { get; set; }
        public DateTime AttemptTime { get; set; }
        public bool IsCorrect { get; set; }
        public string CodeLanguage { get; set; }
        public string Verdict { get; set; }
    }
}
