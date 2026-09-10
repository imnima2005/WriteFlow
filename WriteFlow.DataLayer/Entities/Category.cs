using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.DataLayer.Entities
{
    public class Category:BaseEntity
    {   
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Slug { get; set; }
        
        [MaxLength(300)]
        public string MetaTag { get; set; }
        
        [MaxLength(500)]
        public string MetaDescription { get; set; }
        
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public Category Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }

        [InverseProperty("Category")]
        public ICollection<Post> Posts { get; set; }

        [InverseProperty("SubCategory")]
        public ICollection<Post> SubPosts { get; set; }
        #endregion
    }
}
