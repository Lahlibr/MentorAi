using MentorAi_backd.Domain.Entities.Problems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface ICompiler
    {
        Task<CompileResult> CompileAsync(string code, string workingDirectory);
    }
}
