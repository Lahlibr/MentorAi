using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.ProblemDto
{
    public class TestResultDetail
    {
        public string TestCase { get; set; }
        public bool IsPass { get; set; }
        public string ActualOutput { get; set; }
        public string ExpectedOutput { get; set; }
       
    }
}
