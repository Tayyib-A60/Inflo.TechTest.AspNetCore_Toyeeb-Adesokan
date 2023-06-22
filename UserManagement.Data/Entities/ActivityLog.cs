using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class ActivityLog
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string RequestId { get; set; } = default!;
    public string Path { get; set; } = default!;
    public string Method { get; set; } = default!;
    public int ResponseStatusCode { get; set; } = default!;
}
