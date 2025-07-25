using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ModulesDto
{
     public class UpdateModuleDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(1_000)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? ResourceType { get; set; }

        [StringLength(500)]
        public string? ResourceIdentifier { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

       
        public string? Prerequisites { get; set; }
    }
}
