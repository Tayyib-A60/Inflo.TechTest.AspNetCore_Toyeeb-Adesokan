using System.Linq;
using UserManagement.Core;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet("list")]
    public ViewResult List([FromQuery] UserListQuery filter)
    {
        var model = GetUsers(filter.UserStateFilter);

        return View(model);
    }

    private UserListViewModel GetUsers(UserStateFilter filter)
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DOB = p.DateOfBirth is null ? "N/A" : p.DateOfBirth?.ToString("dd/mm/yyyy")
        });

        if(filter != UserStateFilter.All)
        {
            if(filter == UserStateFilter.Active)
            {
                items = items.Where(item => item.IsActive);
            }
            else if (filter == UserStateFilter.InActive)
            {
                items = items.Where(item => !item.IsActive);
            }
        }

        return new UserListViewModel
        {
            Items = items.ToList()
        };
    }

    [HttpGet("create")]
    public ViewResult Create()
    {
        var model = new CreateUserModel();
        return View(model);
    }

    [HttpPost("create")]
    public ViewResult Create(CreateUserModel input)
    {
        if (ModelState.IsValid)
        {
            _userService.CreateUser(new CreateUserDTO(input.Forename, input.Surname, input.Email, input.Date));
            var users = GetUsers(UserStateFilter.All);
            return View("List", users);
        }

        return View(input);
    }

    [HttpGet]
    public ViewResult Edit(long id)
    {
        var user = _userService.GetUser(id);
        var model = new UpdateUserModel { Id = id};
        if(user is not null)
        {
            model.Forename = user.Forename;
            model.Surname = user.Surname;
            model.Email = user.Email;
        }
        return View("Edit", model);
    }

    [HttpGet, Route("View")]
    public ViewResult View(long id)
    {
        var user = _userService.GetUser(id);
        var model = new UserListItemViewModel { Id = id };
        if (user is not null)
        {
            model.Forename = user.Forename;
            model.Surname = user.Surname;
            model.Email = user.Email;
        }
        return View(model);
    }

    [HttpPost]
    public ViewResult Edit(UpdateUserModel input)
    {
        if(ModelState.IsValid)
        {
            _userService.UpdateUser(new UpdateUserDTO(input.Forename, input.Surname, input.Email, input.Date, input.Id));
            var users = GetUsers(UserStateFilter.All);
            return View("List", users);
        }

        return View(input);
    }

    [HttpDelete("delete/{id}")]
    public ViewResult DeleteUser(long id)
    {
        _userService.DeleteUser(id);
        var users = GetUsers(UserStateFilter.All);
        return View("List", users);
    }
}
