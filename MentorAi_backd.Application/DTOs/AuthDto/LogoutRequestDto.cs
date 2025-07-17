using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.DTOs.AuthDto
{
    public class LogoutRequestDto
    {
        public string RefreshToken { get; set; }
    }
}
