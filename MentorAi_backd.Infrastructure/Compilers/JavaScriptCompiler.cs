using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.Interfaces;
using MentorAi_backd.Domain.Entities.Problems;

namespace MentorAi_backd.Infrastructure.Compilers
{
    public class JavaScriptCompiler : ICompiler
    {
      
        
            public async Task<CompileResult> CompileAsync(string code, string workingDirectory)
            {
                try
                {
                    var fileName = Path.Combine(workingDirectory, "solution.js");
                    await File.WriteAllTextAsync(fileName, code);

                  
                    return new CompileResult
                    {
                        Success = true,
                        ExecutablePath = fileName,
                        Error = null
                    };
                }
                catch (Exception ex)
                {
                    return new CompileResult
                    {
                        Success = false,
                        Error = ex.Message,
                        ExecutablePath = null
                    };
                }
            }
    }
    
}
