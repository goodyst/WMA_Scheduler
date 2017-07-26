using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    
    public class Belt_Constraint : IClass_Constraint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string MinName { get; set; }
        public string MaxName { get; set; }
    }
}
