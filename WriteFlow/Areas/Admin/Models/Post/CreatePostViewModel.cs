using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WriteFlow.Areas.Admin.Models.Post
{
    public class CreatePostViewModel
    {
        [Display(Name="انتخاب دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public int CategoryId { get; set; }

        [Display(Name = "انتخاب دسته بندی")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public string Title { get; set; }

        [Display(Name = "اختصار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public string Slug { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public string Description { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public string ShortDescription { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید. ")]
        public IFormFile ImageFile { get; set; }

        public bool IsSpecial { get; set; }
    }
}
