using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MentorAi_backd.Application.Hubs
{
    public class SubmissionStatusHub : Hub
    {
        public async Task JoinSubmissionGroup(int submissionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"submission-{submissionId}");
        }
        public async Task LeaveSubmissionGroup(int submissionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"submission-{submissionId}");
        }
    }
}
