using MentorAi_backd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.AdminDto
{
    public class ReviewerAdminViewDto
    {
        public int UserId { get; set; }
        public int ReviewerProfileId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ReviewerStatus Status { get; set; }
    }
}
