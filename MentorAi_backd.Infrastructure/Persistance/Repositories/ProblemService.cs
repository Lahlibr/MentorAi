using MentorAi_backd.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MentorAi_backd.Infrastructure.Persistance.Data;
using Microsoft.Extensions.Logging;

namespace MentorAi_backd.Infrastructure.Persistance.Repositories
{
    public class ProblemService 
    {
        private readonly MentorAiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProblemService> _logger;

    }
   
}
