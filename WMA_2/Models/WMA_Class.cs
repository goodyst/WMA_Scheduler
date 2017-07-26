using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMA_2.Models
{
    public partial class WMA_Class
    {

        private List<ClassTimes> _class_Times;
        private List<IClass_Constraint> _constraints;
        private List<Class_Times> _class_Times_Location;

        public WMA_Class()
        {
            init();
        }   


        public int ClassId { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        [StringLength(255)]
        public string Description { get; set; }
        [NotMapped]
        public List<ClassTimes> Class_Times {
            get { 
            if (this._class_Times == null) {
                    // new List<Class_Times>(from ct in db.cl where w.Id == this.CurrentWholesalerId select w).First();
                    this._class_Times = new List<ClassTimes>();
                }
                return _class_Times;
            }
        }
        [NotMapped]
        public List<IClass_Constraint> Contraints { get {
                if (this._constraints == null) {
                    this._constraints = new List<IClass_Constraint>();
                }
                return this._constraints;
            }
        }
        public void init() {
            _class_Times = new List<ClassTimes>();
            _constraints = new List<IClass_Constraint>();
        }
        public void addClassTime(ClassTimes ct) {
            bool insertPosFound = false;
            int len = this._class_Times.Count;
            for (int index = 0; index < len; index++)
            {
                ClassTimes ctTest = this._class_Times[index];
                if (ct.DayOfWeek < ctTest.DayOfWeek)
                {                    
                    this._class_Times.Insert(index, ct);
                    insertPosFound = true;
                    index = this._class_Times.Count;
                }
                else if  (ct.DayOfWeek == ctTest.DayOfWeek)
                {
                    if(ct.StartTime < ctTest.StartTime)
                    {
                        this._class_Times.Insert(index, ct);
                        insertPosFound = true;
                        index = this._class_Times.Count;
                    }
                }
            }
            if (!insertPosFound) { 
                this._class_Times.Add(ct);
            }
        }
        public void addClassTime(List<ClassTimes> ct)
        {
            this._class_Times.AddRange(ct);
        }
        public List<ClassTimes> Class_Times_sorted() {
            int midValue = this._class_Times.Count / 2;
            int firstHalfLen = midValue;
            int secondHalfLen = this._class_Times.Count - midValue;
            return this.merge_sort(this._class_Times.GetRange(0, firstHalfLen), this._class_Times.GetRange(midValue, secondHalfLen));
            
        }
        private List<ClassTimes> merge_sort(List<ClassTimes> list1, List<ClassTimes> list2)
        {
            if(list1.Count > 1)
            {
                int midValue = list1.Count / 2;
                int firstHalfLen = midValue;
                int secondHalfLen = list1.Count - midValue;
                list1 = this.merge_sort(list1.GetRange(0, firstHalfLen), list1.GetRange(midValue, secondHalfLen));
            }
            if (list2.Count > 1)
            {
                int midValue = list2.Count / 2;
                int firstHalfLen = midValue;
                int secondHalfLen = list2.Count - midValue;
                list2 = this.merge_sort(list2.GetRange(0, firstHalfLen), list2.GetRange(midValue, secondHalfLen));
            }
            List<ClassTimes> lstNew = new List<ClassTimes>();
            foreach(ClassTimes ct1 in list1)
            {
                foreach(ClassTimes ct2 in list2)
                {
                    if (ct1.DayOfWeek < ct2.DayOfWeek)
                    {
                        lstNew.Add(ct1);
                    }
                    else if (ct1.DayOfWeek < ct2.DayOfWeek)
                    {
                        lstNew.Add(ct2);
                    }
                    else {
                        if (ct1.StartTime < ct2.StartTime)
                        {
                            lstNew.Add(ct1);
                        } else
                        {
                            lstNew.Add(ct2);
                        }
                    }
                }
            }
            return lstNew;
        }
        /** todo Class_Times prop in this Class is only class and times
         * referring to ClassTime class
         * Full_Class_Times_Info prop will refer to all associated properties
         * right now it only refers to class->time->location connection
         * */
        [NotMapped]
        public List<Class_Times> Full_Class_Time_Info
        {
            get
            {
                if (this._class_Times_Location == null)
                {
                    // new List<Class_Times>(from ct in db.cl where w.Id == this.CurrentWholesalerId select w).First();
                    this._class_Times_Location = new List<Class_Times>();
                }
                return _class_Times_Location;
            }
            set {
                this._class_Times_Location = value;
            }
        }

    }
}
