using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    public class ClassView_Edit_Return
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ClassView_ClassTimes_Edit_Return> class_times { get; set; }

    }
}
