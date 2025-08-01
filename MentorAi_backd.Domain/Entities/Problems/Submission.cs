using MentorAi_backd.Domain.Entities.Student;
using MentorAi_backd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class Submission
    {
       
            [Key]
            public int Id { get; set; }

          
            public int StudentId { get; set; } 

          
            public int ProblemId { get; set; }
            
            public string Code { get; set; }
            public string Language { get; set; } 

            public DateTime SubmissionTime { get; set; }
            public DateTime ProblemStartTime { get; set; }

            public bool IsCorrect { get; set; }
            public int AttemptsCount { get; set; }

            // Grading status
            public string Status { get; set; } = "Pending"; 
            public string CompileError { get; set; }
            public int ExecutionTime { get; set; } 
            public int MemoryUsed { get; set; } 

            public virtual Problem Problem { get; set; } = default!;
            public virtual StudentProfile Student { get; set; } = default!;

            public ICollection<TestCaseResultEntity> TestCaseResults { get; set; }


    }
}

