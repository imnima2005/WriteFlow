using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteFlow.DataLayer.Entities
{
    public class User:BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Password { get; set; }

        [Required]
        [MaxLength(300)]
        public string FullName { get; set; }


        public UserRole Role { get; set; }

        #region Relations
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        #endregion
    }

    public enum UserRole
    {
        Admin,
        User,
        Writer
    }
}
