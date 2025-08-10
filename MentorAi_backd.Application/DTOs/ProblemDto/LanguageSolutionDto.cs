using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class LanguageSolutionDto
    {
        [Required]
        public string Language { get; set; }

        [Required]
        [MinLength(5)]
        public string SolutionTemplate { get; set; }
    }
}
