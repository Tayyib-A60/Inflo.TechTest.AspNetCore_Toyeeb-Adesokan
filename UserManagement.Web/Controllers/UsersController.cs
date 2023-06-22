using System.Linq;
using System.Threading.Tasks;
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
        var allUsers = new List<UserListItemViewModel>();

        if (filter.UserStateFilter == UserStateFilter.All)
        {
            return View(GetUsers());
        }

        var filteredUsers = _userService
            .FilterByActive(filter.UserStateFilter == UserStateFilter.Active ? true : false)
            .Select(u => new UserListItemViewModel
            {
                Id = u.Id,
                Forename = u.Forename,
                Surname = u.Surname,
                Email = u.Email,
                IsActive = u.IsActive,
                DOB = u.DateOfBirth is null ? "N/A" : u.DateOfBirth?.ToString()
            }).OrderBy(u => u.Id).ToList();

        return View(new UserListViewModel
        {
            Items = filteredUsers
        });
    }

    private UserListViewModel GetUsers()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DOB = p.DateOfBirth is null ? "N/A" : p.DateOfBirth?.ToString()
        });

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
    public async Task<ViewResult> Create(CreateUserModel input)
    {
        if (ModelState.IsValid)
        {
            await _userService.CreateUser(new CreateUserDTO(input.Forename, input.Surname, input.Email, input.Date));
            var users = GetUsers();
            return View("List", users);
        }

        return View(input);
    }

    [HttpGet]
    public async Task<ViewResult> Edit(long id)
    {
        var user = await _userService.GetUser(id);
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
    public async Task<ViewResult> View(long id)
    {
        var user = await _userService.GetUser(id);
        var model = new UserListItemViewModel { Id = id };
        if (user is not null)
        {
            model.Forename = user.Forename;
            model.Surname = user.Surname;
            model.Email = user.Email;
            model.DOB = user.DateOfBirth.ToString();
        }
        return View(model);
    }

    [HttpPost]
    public async Task<ViewResult> Edit(UpdateUserModel input)
    {
        if(ModelState.IsValid)
        {
            await _userService.UpdateUser(new UpdateUserDTO(input.Forename, input.Surname, input.Email, input.Date, input.Id));
            var users = GetUsers();
            return View("List", users);
        }

        return View(input);
    }

    [HttpDelete("delete/{id}")]
    public ViewResult DeleteUser(long id)
    {
        _userService.DeleteUser(id);
        var users = GetUsers();
        return View("List", users);
    }
}
