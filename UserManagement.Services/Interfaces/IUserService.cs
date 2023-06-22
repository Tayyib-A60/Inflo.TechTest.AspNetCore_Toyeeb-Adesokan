using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();
    Task CreateUser(CreateUserDTO userDto);
    Task UpdateUser(UpdateUserDTO userDto);
    Task DeleteUser(long id);
    Task<User?> GetUser(long id);
}
