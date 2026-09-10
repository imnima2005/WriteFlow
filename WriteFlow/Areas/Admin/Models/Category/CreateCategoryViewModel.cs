using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WriteFlow.Areas.Admin.Models.Category
{
    public class CreateCategoryViewModel
    {
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="وارد کردن عنوان اجباری است.")]
        public string Title { get; set; }
        [Display(Name = "slug")]
        [Required(ErrorMessage = "وارد کردن slug اجباری است.")]
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }
    }
}
