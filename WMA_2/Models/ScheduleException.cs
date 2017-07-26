using System;
using System.Collections.Generic;

namespace WMA_2.Models
{
    public partial class ScheduleException
    {
        public int ScheduleExceptionId { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public DateTime ClassDate { get; set; }
        public int Position { get; set; }
    }
}
