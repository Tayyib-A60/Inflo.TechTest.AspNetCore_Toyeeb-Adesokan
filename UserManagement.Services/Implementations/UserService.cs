using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;
public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        var users = _dataAccess.GetAll<User>().Where(u => u.IsActive == isActive).ToList();
        return users;
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public async Task CreateUser(CreateUserDTO createUserDTO)
    {
        var user = new User
        {
            Email = createUserDTO.Email,
            Forename = createUserDTO.Forename,
            Surname = createUserDTO.Surname,
            IsActive = true,
            DateOfBirth = DateTime.Parse(createUserDTO.Date)
        };
        await _dataAccess.CreateAsync<User>(user);
    }

    public async Task DeleteUser(long id)
    {
        var user = _dataAccess.Get<User>(id);

        if (user is not null)
        {
            await _dataAccess.DeleteAsync<User>(user);
        }
    }

    public async Task UpdateUser(UpdateUserDTO updateUserDTO)
    {
        var user = _dataAccess.Get<User>(updateUserDTO.Id);

        if(user is not null)
        {
            user.Id = updateUserDTO.Id;
            user.Email = updateUserDTO.Email;
            user.Forename = updateUserDTO.Forename;
            user.Surname = updateUserDTO.Surname;
            user.IsActive = true;
            user.DateOfBirth = DateTime.Parse(updateUserDTO.Date);

            await _dataAccess.UpdateAsync<User>(user);
        }
    }

    public async Task<User?> GetUser(long id)
    {
        return await _dataAccess.GetAsync<User>(id);
    }
}
