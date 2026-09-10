using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WriteFlow.Areas.Admin.Models.Category
{
    public class EditCategoryViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است.")]
        public string Title { get; set; }
        [Display(Name = "slug")]
        [Required(ErrorMessage = "وارد کردن slug اجباری است.")]
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public int Id { get; set; }
    }
}
