using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.DTOs.Users;
using WriteFlow.DataLayer.Entities;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.DataLayer.Context;


namespace WriteFlow.CoreLayer.Services.Users
{
    public class UserService:IUserServices
    {
        private readonly WriteFlowContext _context;
        public UserService(WriteFlowContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است.");

            var passwordHash = Md5Helper.Encode(registerDto.Password);

            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Password = passwordHash,
                Role = UserRole.User
            });

            _context.SaveChanges();
            return OperationResult.Success();
        }
        public OperationResult CreateUser(CreateUserDto createUser)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == createUser.UserName);

            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است.");

            UserRole roleEnum = (UserRole)Enum.Parse(typeof(UserRole), createUser.Role, ignoreCase: true);
            var passwordHash = Md5Helper.Encode(createUser.Password);

            _context.Users.Add(new User()
            {
                FullName = createUser.FullName,
                UserName = createUser.UserName,
                Password = passwordHash,
                Role = roleEnum
            });

            _context.SaveChanges();
            return OperationResult.Success();
        }


        public UserDto LoginUser(UserLoginDto loginDto)
        {
            var passwordHash = Md5Helper.Encode(loginDto.Password);
            var user = _context.Users.FirstOrDefault(user => user.UserName == loginDto.UserName && user.Password == passwordHash);
            if (user == null)
                return null;
            var userDto = new UserDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role.ToString(),
                UserName = user.UserName
            };
            return userDto;
        }

        public UserFilterDto GetUserByFilter(UserFilterParams filterParams)
        {
            var result = _context.Users
                .OrderByDescending(d => d.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.UserName))
                result = result.Where(r => r.UserName.Contains(filterParams.UserName));

            var skip = (filterParams.PageId - 1) * filterParams.Take;
            var model = new UserFilterDto()
            {
                Users = result.Skip(skip).Take(filterParams.Take)
                    .Select(user => new UserDto()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FullName = user.FullName,
                        Password = Md5Helper.Encode(user.Password),
                        Role = user.Role.ToString()
                    }).ToList(),
                FilterParams = filterParams
            };

            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
            return model;
        }

        public UserDto GetUserById(int userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                Password = Md5Helper.Encode(user.Password)
            };
        }

        public OperationResult EditUser(EditUserDto dataObject)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == dataObject.UserId);

            if (user == null)
                return OperationResult.NotFound();

            user.UserName = dataObject.UserName;
            user.FullName = dataObject.FullName;
            user.Role = dataObject.Role;

            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return OperationResult.NotFound("user doesn't found.");
            user.IsDelete = true;
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
}
