namespace MentorAi_backd.Application.DTOs.ProfileDto
{
    public class StudentProfileDto : BaseUserProfileDto
    {
        
        public int Age { get; set; }
        public int OverallMarks { get; set; } 
        public int ProblemsSolvedCount { get; set; } 
        public int TotalChallengesAttempted { get; set; }

        // Current learning status
        public string? CurrentRoadmapTitle { get; set; }
        public double CurrentRoadmapProgressPercentage { get; set; } 
        public string? LastActiveModuleTitle { get; set; }
        public DateTime? LastActivityDate { get; set; } 
        public int CertificationsEarnedCount { get; set; }
        public string? Status { get; set; } 
        public bool IsEmailVerified { get; set; }
        public double? AssessmentScore { get; set; }
        public string? CurrentLearningGoal { get; set; }
        public string? PreferredLearningStyle { get; set; }

    }
}
