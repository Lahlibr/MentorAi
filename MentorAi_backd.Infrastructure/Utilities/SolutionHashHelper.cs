using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Utilities
{
    public static class SolutionHashHelper
    {
        public static string ComputeHash(string solution)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(solution ?? string.Empty);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
