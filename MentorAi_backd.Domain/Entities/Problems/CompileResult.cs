using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Domain.Entities.Problems
{
    public class CompileResult
    {
        public bool Success { get; set; }              
        public string ExecutablePath { get; set; }    
        public string Error { get; set; }             
    }
}
