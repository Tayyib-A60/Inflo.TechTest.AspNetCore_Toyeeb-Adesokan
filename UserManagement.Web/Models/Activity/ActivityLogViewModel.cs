using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Models.Activity;

public class ActivityLogViewModel
{
    public long Id { get; set; }
    public string RequestId { get; set; } = default!;
    public string Path { get; set; } = default!;
    public string Method { get; set; } = default!;
    public int ResponseStatusCode { get; set; } = default!;
}

public class ActivityListLogViewModel
{
    public List<ActivityLogViewModel> Items { get; set; } = new();
    public int Count { get; set; }
}

public class ActivityQuery
{
    public int page { get; set; }
    public int pageSize { get; set; }
}
