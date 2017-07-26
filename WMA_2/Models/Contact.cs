using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public int UserId { get; set; }
        public int ContactMethod { get; set; }
        public string Description { get; set; }
    }
}
