using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using System.Linq;
using UserManagement.Services.Extensions;

namespace UserManagement.Services.Domain.Implementations;

public class ActivityService : IActivityService
{
    private readonly IDataContext _dataAccess;
    public ActivityService(IDataContext dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public (IEnumerable<ActivityLog>, int) GetActivityLogs(int page, int pageSize)
    {
        var activityLogs = new List<ActivityLog>();
        int count = 0;
        pageSize = pageSize > 0 ? pageSize : 10; 
        int pagesToSkip = page > 0 ? (page - 1) * pageSize: 0;
        try
        {
            var logs = _dataAccess.GetAll<ActivityLog>();
            count = logs.Count();
            activityLogs = logs.OrderBy(a => a.Id).ApplyPaging(page, pageSize).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return (activityLogs, count);
    }

    public async Task TrackActivity(ActivityLogDTO activityLogDto)
    {
        try
        {
            await _dataAccess.CreateAsync<ActivityLog>(new ActivityLog
            {
                RequestId = activityLogDto.RequestId,
                Path = activityLogDto.Path,
                Method = activityLogDto.Method,
                ResponseStatusCode = activityLogDto.ResponseStatusCode
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
