using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ModulesDto
{
    public class ModuleDto
    {
        public int Id { get; set; }
        public int RoadmapId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string ResourceType { get; set; } = default!;       
        public string ResourceIdentifier { get; set; } = default!; 
        public string Status { get; set; } = "NotStarted";        
        public int Order { get; set; }
        public string? Prerequisites { get; set; }
    }
}
