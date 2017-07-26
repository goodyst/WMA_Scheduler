using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMA_2.Models
{
    public enum ClassType { Special, Scheduled }
    public class ClassTimes
    {
        public int Id { get; set; }
        [Display (Name = "Day Of Week")]
        public DayOfWeek DayOfWeek { get; set; }
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
        [Display(Name = "Class Type")]
        public ClassType ClassType { get; set; }
        [NotMapped]
        [Display(Name = "Start Time")]
        public String StartTimeText {
            get {
                return DateTime.ParseExact(StartTime.ToString().Substring(0, 5), "HH:mm", null).ToString("h:mm tt");
             }
            set
            {
                try {
                    DateTime t = DateTime.ParseExact(value, "h:mm tt", CultureInfo.InvariantCulture);
                    this.StartTime = t.TimeOfDay;
                }
                catch { }

            }
        }
        [NotMapped]
        [Display(Name = "End Time")]
        public String EndTimeText { 
        get {
                return DateTime.ParseExact(EndTime.ToString().Substring(0, 5), "HH:mm", null).ToString("h:mm tt");
            }
            set
            {
                try {
                    DateTime t = DateTime.ParseExact(value, "h:mm tt", CultureInfo.InvariantCulture);
                    this.EndTime = t.TimeOfDay;
                }
                catch { }

            }
        }
        public override string ToString()
        {
            if (this.DayOfWeek == DayOfWeek.Sunday && StartTime.Ticks == 0 && EndTime.Ticks == 0) {
                return "";
            }
            StringBuilder sb = new StringBuilder(this.DayOfWeek + " " + this.StartTimeText + "-" + this.EndTimeText);
            return sb.ToString();
        }
    }
}
