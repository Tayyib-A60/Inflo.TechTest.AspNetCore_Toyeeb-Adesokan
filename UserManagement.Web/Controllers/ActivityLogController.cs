using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Activity;

namespace UserManagement.WebMS.Controllers;

[Route("activityLog")]
public class ActivityLogController : Controller
{
    private readonly IActivityService _activityService;

    public ActivityLogController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet("list")]
    public ViewResult List([FromQuery] ActivityQuery query)
    {
        var result = _activityService.GetActivityLogs(query.page, query.pageSize);
        var logs = result.Item1.Select(a => new ActivityLogViewModel
        {
            Id = a.Id,
            RequestId = a.RequestId,
            Method = a.Method,
            ResponseStatusCode = a.ResponseStatusCode,
            Path = a.Path
        }).ToList();

        return View(new ActivityListLogViewModel
        {
            Items = logs,
            Count = result.Item2
        });
    }
}
