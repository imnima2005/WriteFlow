using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.DataLayer.Entities
{
    public class Post:BaseEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; } = null;

        [Required]
        public string ImageName { get; set; }

        public int VisitCount { get; set; } = 0;

        public bool IsSpecial { get; set; }

        [Required]
        [MaxLength(80)]
        public string ShortDescription { get; set; }


        #region Relations
        public ICollection<PostComment> PostComments{ get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Posts")]
        public Category Category { get; set; }

        [ForeignKey("SubCategoryId")]
        [InverseProperty("SubPosts")]
        public Category SubCategory { get; set; }
        #endregion


    }
}
