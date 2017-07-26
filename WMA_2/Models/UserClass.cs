using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class UserClass
    {
        public int UserClassId { get; set; }
        public int ClassId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
