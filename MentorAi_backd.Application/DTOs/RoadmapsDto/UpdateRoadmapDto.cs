using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.RoadmapDto
{
    public class UpdateRoadmapDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(1_000)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
