using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    public class WMA_Constant_Category
    {
        private int constantCategory;
        
        public WMA_Constant_Category()
        {
        }
        public WMA_Constant_Category(int constantCategory)
        {
            this.constantCategory = constantCategory;
            this.Id = constantCategory;
        }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}
