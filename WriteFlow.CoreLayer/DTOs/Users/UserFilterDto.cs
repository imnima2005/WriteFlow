using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WriteFlow.CoreLayer.Utilities;

namespace WriteFlow.CoreLayer.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
        public UserFilterParams FilterParams { get; set; }
    }

    public class UserFilterParams
    {
        public string UserName { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}
