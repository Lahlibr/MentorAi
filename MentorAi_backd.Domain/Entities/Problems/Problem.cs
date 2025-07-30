using MentorAi_backd.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Enums;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class Problem
    {
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; } // Stored as nvarchar(max) or similar

        public DifficultyLevelEnum Difficulty { get; set; }

        public virtual ICollection<Submission>Submission { get; set; } = new List<Submission>();
        public virtual ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();







    }
}

