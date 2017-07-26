using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    public interface IClass_Constraint
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Min { get; set; }
        int Max { get; set; }
        string MinName { get; set; }
        string MaxName { get; set; }
    }
}
