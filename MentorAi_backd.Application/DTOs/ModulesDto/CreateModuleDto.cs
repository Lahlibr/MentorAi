using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ModulesDto
{
    public class CreateModuleDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1_000)]
        public string? Description { get; set; }

        [Required, StringLength(50)]
        public string ResourceType { get; set; } = default!;

        [Required, StringLength(500)]
        public string ResourceIdentifier { get; set; } = default!;

       
        public string? Prerequisites { get; set; }
    }
}
