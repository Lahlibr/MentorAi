using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class ExecuteCodeDto
    {
        [Required]
        public string Language { get; set; }

        [Required]
        public string Code { get; set; }

        public string Input { get; set; } = "";
    }
}
