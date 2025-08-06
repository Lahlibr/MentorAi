using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Application.Interfaces
{
    public interface IAnalyticsService
    {
        Task GenerateStudentPerformanceReport();
        Task ExportAllAnalyticsToCsv();
        Task ShowProblemStatistics();
        Task ShowLeaderboard();
    }
}
