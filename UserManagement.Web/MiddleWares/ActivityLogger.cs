using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UserManagement.Core;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Web.MiddleWares
{
    public class ActivityLogger
    {
        private readonly RequestDelegate _next;
        private readonly IActivityService _activityService;

        public ActivityLogger(RequestDelegate next, IActivityService activityService)
        {
            _next = next;
            _activityService = activityService;
        }

        public async Task Invoke(HttpContext context)
        {
            var data = new ActivityLogDTO
            {
                RequestId = context.TraceIdentifier,
                Method = context.Request.Method,
                Path = context.Request.Path
            };

            await _next(context);

            data.ResponseStatusCode = context.Response.StatusCode;
            await _activityService.TrackActivity(data);
        }
    }
}

