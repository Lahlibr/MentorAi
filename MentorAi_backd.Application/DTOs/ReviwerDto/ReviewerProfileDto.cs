using MentorAi_backd.Application.DTOs.BaseDto;
using MentorAi_backd.Application.DTOs.ProfileDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ReviwerDto
{
    public class ReviewerProfileDto : BaseUserProfileDto
    {
        public int TotalReviewsConducted { get; set; }
        public string Bio { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; } = 0;

        public string Availability { get; set; } = "Full-time";

        public string ExpertiseAreasJson { get; set; } = "[]";

        public double AverageRating { get; set; } = 0.0;
    }
}
