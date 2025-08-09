using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.StudentDto
{
    public class CertificationDto
    {
        public int CertificationId { get; set; }
        public string Title { get; set; }
        public DateTime EarnedDate { get; set; }
    }
}
