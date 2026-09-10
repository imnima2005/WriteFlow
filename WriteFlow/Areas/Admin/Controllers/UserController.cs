using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteFlow.Areas.Admin.Models.User;
using WriteFlow.CoreLayer.DTOs.Users;
using WriteFlow.CoreLayer.Services.Users;
using WriteFlow.CoreLayer.Utilities;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public IActionResult Index(int pageId = 1, string userName=null)
        {
            var param = new UserFilterParams()
            {
                PageId = pageId,
                UserName = userName,
                Take = 1
            };
            var model = _userServices.GetUserByFilter(param);
            return View(model);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var result = _userServices.CreateUser(new CreateUserDto()
            {
                UserName = viewModel.UserName,
                FullName = viewModel.FullName,
                Password = viewModel.Password,
                Role = viewModel.Role
            });

            if (result.Status != OperationResultStatus.Success)
                return View(viewModel);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user == null)
                return RedirectToAction("Index");

            var model = new EditUserViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            UserRole roleEnum = (UserRole)Enum.Parse(typeof(UserRole), viewModel.Role, ignoreCase: true);
            var result = _userServices.EditUser(new EditUserDto()
            {
                UserId = viewModel.UserId,
                UserName = viewModel.UserName,
                FullName = viewModel.FullName,
                Role = roleEnum
            });

            if (result.Status != OperationResultStatus.Success)
                return View(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int userId)
        {
            var result = _userServices.DeleteUser(userId);
            if (result.Status == OperationResultStatus.NotFound)
                return NotFound();
            return RedirectToAction("Index");
        }
    }
}
