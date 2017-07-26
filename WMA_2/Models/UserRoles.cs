using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class UserRoles
    {
        public int UserRoleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
