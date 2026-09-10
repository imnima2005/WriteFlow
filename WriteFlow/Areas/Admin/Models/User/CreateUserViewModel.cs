using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WriteFlow.Areas.Admin.Models.User
{
    public class CreateUserViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبور و تکرار آن یکسان نیست. ")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
