using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MediatR;

namespace MentorAi_backd.Application.Commands
{
    public record CreateSubmissionCommand(CreateSubmissionDto SubmissionDto) : IRequest<int>
    {
        
    }
}
