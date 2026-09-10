using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Users;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.CoreLayer.Services.Users
{
    public interface IUserServices
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(UserLoginDto loginDto);
        OperationResult CreateUser(CreateUserDto createUser);
        UserFilterDto GetUserByFilter(UserFilterParams filterParams);
        UserDto GetUserById(int userId);
        OperationResult EditUser(EditUserDto dataObject);
        OperationResult DeleteUser(int userId);
    }
}
