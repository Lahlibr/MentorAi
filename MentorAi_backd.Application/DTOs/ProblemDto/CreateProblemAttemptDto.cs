using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class CreateProblemAttemptDto
    {
        [Required]
        [MinLength(10, ErrorMessage = "Code cannot be empty.")]
        public string StudentCode { get; set; }

        [Required]
        [StringLength(20)]
        public string Language { get; set; } 
    }
}
