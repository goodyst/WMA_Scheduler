using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    /** the class that connects the constraints
     * with the class */
    public class Class_Constraint
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int ConstraintId { get; set; }
        public int ConstraintType { get; set; }
    }
}
