using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Application.DTOs.ProblemDto;
using MediatR;

namespace MentorAi_backd.Application.Commands
{
    public record CreateSubmissionCommand : IRequest<int>
    {
        public int ProblemId { get; set; }
        public int UserId { get; set; }

        public string Code { get; set; }    
        public string Language { get; set; }
        public CreateSubmissionCommand(CreateSubmissionDto dto)
        {
            ProblemId = dto.ProblemId;
            UserId = dto.StudentId; // assuming StudentId maps to UserId in your system
            Code = dto.Code;
            Language = dto.Language;
        }
    }
   
    }
