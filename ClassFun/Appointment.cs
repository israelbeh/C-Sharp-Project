//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassFun
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public Appointment()
        {
            this.ScheduledFors = new HashSet<ScheduledFor>();
        }
    
        public int AppointmentID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<int> Duration { get; set; }
        public int CounselorID { get; set; }
        public int EmployeeID { get; set; }
        public int RoomNumber { get; set; }
        public string Notes { get; set; }
    
        public virtual Counselor Counselor { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<ScheduledFor> ScheduledFors { get; set; }
    }
}