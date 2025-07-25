using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class RoadmapModule
    {
        public int RoadmapId { get; set; }
        public Roadmap Roadmap { get; set; }

        public int ModuleId { get; set; }
        public LearningModule LearningModule { get; set; }

        public int Order { get; set; }
    }
}
