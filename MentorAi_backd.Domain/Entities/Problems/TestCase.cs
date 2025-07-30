using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class TestCase
    {
        public int Id { get; set; }
        public int ProblemId { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public bool IsHidden { get; set; } 

        public virtual Problem Problem { get; set; }
    }
}
