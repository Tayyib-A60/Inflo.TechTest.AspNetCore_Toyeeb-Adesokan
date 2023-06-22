using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IActivityService
{
    Task TrackActivity(ActivityLogDTO activityLogDto);
    (IEnumerable<ActivityLog>, int) GetActivityLogs(int page, int pageSize);
}
