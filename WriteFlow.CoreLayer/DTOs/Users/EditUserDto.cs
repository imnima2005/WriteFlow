using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.CoreLayer.DTOs.Users
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public string UserName { get; set; }
    }
}
