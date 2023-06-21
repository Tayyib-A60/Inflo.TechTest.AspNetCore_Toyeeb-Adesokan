using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class CreateUserModel
{
    [Required(ErrorMessage = "Forename is required")]
    public string Forename { get; set; } = default!;
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; } = default!;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email entered is invalid")]
    public string Email { get; set; } = default!;
    [Required(ErrorMessage = "DOB is required")]
    [DisplayFormat(DataFormatString = "dd/mm/yyyy", ApplyFormatInEditMode = true)]
    public string Date { get; set; } = default!;
}

public class UpdateUserModel: CreateUserModel
{
    public long Id { get; set; } = default!;
}

public enum UserStateFilter
{
    All,
    Active,
    InActive
}

public class UserListQuery
{
    public UserStateFilter UserStateFilter { get; set; }
}
