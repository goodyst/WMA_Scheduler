using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMA_2.Models
{
    public partial class WMA_Constant
    {
        public int ConstantId { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int ConstantCategory { get; set; }
        [NotMapped]
        public List<WMA_Constant_Category> Categories { get; set; }
        [NotMapped]
        public WMA_Constant_Category Category { get; set; }
        [NotMapped]
        public String CategoryDescription { get {
                WMA_Constant_Category cat = new WMA_Constant_Category(this.ConstantCategory);
                return cat.Description;
            }

         }
        public SelectList CategoryList() {
            var cats1 = from c in Categories select new SelectListItem {
                Text = c.Description,
                Value = c.Id.ToString()
            };
            // var cats = this.Categories.Select(x => new SelectListItem { Text = x.Description, Value = x.Description });
            var lst = new SelectList(Categories, "Id", "Description", new WMA_Constant_Category(ConstantCategory));
            
            //return new SelectList(cats1, "Id", "Description");
            return lst;
        }
     }  
}
