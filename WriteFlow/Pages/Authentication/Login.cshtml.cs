using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WriteFlow.CoreLayer.Services.Users;
using WriteFlow.CoreLayer.DTOs.Users;

namespace WriteFlow.Pages.Authentication
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly IUserServices _userServices;

        public LoginModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage = "نام کاربری خود را وارد کنید.")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage ="پسوورد خود را وارد کنید")]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = _userServices.LoginUser(new UserLoginDto()
            {
                Password = Password,
                UserName = UserName
            });

            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربر یافت نشد.");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString())

            };

            var identify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identify);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("../Index");

        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
