using MentorAi_backd.Application.DTOs.ProfileDto;
using MentorAi_backd.Application.DTOs.StudentDto;
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
        Task<ApiResponse<StudentProfileDto>> UpdateStudentProfileAsync(int userId, StudentUpdateDto studentProfileDto);

        Task<ApiResponse<StudentProfileDashboardDto>> GetDashboardAsync(int userId);
    }
}
