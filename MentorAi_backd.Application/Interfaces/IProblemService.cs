using MentorAi_backd.Application.DTOs.ProblemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IProblemService
    {
        Task<IEnumerable<ProblemDto>> GetAllProblemAsync(string? difficulty = null, string? category = null);
    }
}
