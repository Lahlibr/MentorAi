namespace MentorAi_backd.Models.Entity
{
    public class Module : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInRoadmap { get; set; } 

        // Foreign Key to Roadmap
        public int RoadmapId { get; set; }
        public Roadmap Roadmap { get; set; } = default!; 

        
        public ICollection<Problem> Problems { get; set; } = new List<Problem>();
        
    }
}
