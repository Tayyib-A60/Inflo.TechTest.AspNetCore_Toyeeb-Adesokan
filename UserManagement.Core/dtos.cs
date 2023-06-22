using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core;

public class CreateUserDTO
{
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; } = default!;
    public string Date { get; set; }

    public CreateUserDTO(string forename, string surname, string email, string date)
    {
        Forename = forename;
        Surname = surname;
        Email = email;
        Date = date;
    }
}

public record ActivityLogDTO
{
    public string RequestId { get; set; } = default!;
    public string Path { get; set; } = default!;
    public string Method { get; set; } = default!;
    public int ResponseStatusCode { get; set; } = default!;
}

public class UpdateUserDTO : CreateUserDTO
{
    [Required]
    public long Id { get; set; }

    public UpdateUserDTO(string forename, string surname, string email, string date, long id): base(forename, surname, email, date)
    {
        Id = id;
    }
}

