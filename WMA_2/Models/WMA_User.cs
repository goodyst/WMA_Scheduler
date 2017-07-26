using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class WMA_User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
    }
}
