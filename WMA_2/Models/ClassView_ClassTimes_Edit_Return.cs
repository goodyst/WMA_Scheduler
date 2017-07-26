using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    public enum EditMode { Add, Edit, Remove }
    public class ClassView_ClassTimes_Edit_Return
    {
        public int id { get; set; }
        public string text { get; set; }
        public EditMode mode { get; set; }
        public int classtime_id { get; set; }
        public int location_id { get; set; }

    }
}
