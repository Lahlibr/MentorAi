using MentorAi_backd.Domain.Entities.Main;


namespace MentorAi_backd.Domain.Entities.UserEntity
{
    public class LearningModule : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInRoadmap { get; set; } 

        // Foreign Key to Roadmap
       
       
        public ICollection<RoadmapModule> RoadmapModules { get; set; }
        public ICollection<Problem> Problems { get; set; } = new List<Problem>();
        
    }
}
