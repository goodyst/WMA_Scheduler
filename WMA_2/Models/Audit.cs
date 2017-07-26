using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class Audit
    {
        public int AuditId { get; set; }
        public string TableName { get; set; }
        public int TableId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string Changes { get; set; }
    }
}
