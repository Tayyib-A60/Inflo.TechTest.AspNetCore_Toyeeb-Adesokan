using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Activity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserManagement.WebMS.Controllers;

[Route("[controller]")]
public class ActivityController : ControllerBase // FOR SPA App
{
    private readonly IActivityService _activityService;

    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet("list_activities")]
    public IActionResult GetActivityLogs([FromQuery] ActivityQuery query)
    {
        var activities = _activityService.GetActivityLogs(query.page, query.pageSize);
        return Ok(activities);
    }
}
