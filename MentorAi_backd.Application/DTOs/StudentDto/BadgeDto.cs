using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class BadgeDto
    {
        public int BadgeId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}
