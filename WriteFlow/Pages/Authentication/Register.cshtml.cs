using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using WriteFlow.CoreLayer.DTOs.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WriteFlow.CoreLayer.Services.Users;

namespace WriteFlow.Pages.Authentication
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly IUserServices _userServices;
        public RegisterModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #region properties
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [MinLength(8, ErrorMessage = "تعداد کاراکتر وارد شده باید حداقل 8 تا باشد.")]
        public string Password { get; set; }
        #endregion
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var result = _userServices.RegisterUser(new UserRegisterDto()
            {
                UserName = UserName,
                FullName = FullName,
                Password = Password
            });
            if (result.Status == CoreLayer.Utilities.OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
