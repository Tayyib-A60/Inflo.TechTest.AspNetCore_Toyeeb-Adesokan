using System.Collections.Generic;
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
    void CreateUser(CreateUserDTO userDto);
    void UpdateUser(UpdateUserDTO userDto);
    void DeleteUser(long id);
    User? GetUser(long id);
}
