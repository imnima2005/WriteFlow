using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.DataLayer.Entities;

namespace WriteFlow.CoreLayer.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
