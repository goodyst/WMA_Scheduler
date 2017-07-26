using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMA_2.Models
{
    public class ClassView
    {
        public WMA_Class Class { get; set; }
        public List<ClassTimes> ClassTimes { get; set; }
        public List<IClass_Constraint> Constraints { get; set; }
        public List<WMA_Class> ClassList { get; set; }
        public List<ClassTimes> ClassTimeList { get; set; }
        public List<Class_Times> Class_Times { get; set; }
        public List<WMA_Location> Location { get; set; }
        public IEnumerable<SelectListItem> DDLClassTimes { get; set; }
        public IEnumerable<SelectListItem> DDLLocations { get; set; }

    }
}
