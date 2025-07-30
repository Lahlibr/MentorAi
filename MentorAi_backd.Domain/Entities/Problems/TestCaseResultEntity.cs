using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class TestCaseResultEntity
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int TestCaseId { get; set; }

        public bool Passed { get; set; }
        public string ActualOutput { get; set; }
        public string ErrorDetails { get; set; }
        public int ExecutionTime { get; set; }

        public virtual Submission Submission { get; set; }
        public virtual TestCase TestCase { get; set; }
    }
}
