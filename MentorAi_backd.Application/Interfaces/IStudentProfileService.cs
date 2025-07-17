using MentorAi_backd.Application.DTOs.ProfileDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IStudentProfileService
    {
        Task<ApiResponse<StudentProfileDto>> GetStudentProfileAsync(int userId);
    }
}
