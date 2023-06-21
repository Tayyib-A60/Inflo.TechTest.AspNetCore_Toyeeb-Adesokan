using System;
using System.Collections.Generic;
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
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public void CreateUser(CreateUserDTO createUserDTO)
    {
        var user = new User
        {
            Email = createUserDTO.Email,
            Forename = createUserDTO.Forename,
            Surname = createUserDTO.Surname,
            IsActive = true,
            DateOfBirth = DateTime.Parse(createUserDTO.Date)
        };
        _dataAccess.Create<User>(user);
    }

    public void DeleteUser(long id)
    {
        var user = _dataAccess.Get<User>(id);

        if (user is not null)
        {
            _dataAccess.Delete<User>(user);
        }
    }

    public void UpdateUser(UpdateUserDTO updateUserDTO)
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

            _dataAccess.Update<User>(user);
        }
    }

    public User? GetUser(long id)
    {
        return _dataAccess.Get<User>(id);
    }
}
