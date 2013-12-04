using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HrsVerification.Models
{
    [Table("HR_HW_WorkedTime")]
    public class WorkedTime   
    {
        public int  WorkedTimeId    { get; set; }

        public string WorkBranchOrgId { get; set; }

        public int SundayNum { get; set; }
        public int SHAHrs { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yy}")]
        public DateTime ScheduleFrom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yy}")]
        public DateTime ScheduleTo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yy}")]
        public DateTime? LunchFrom { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yy}")]
        public DateTime? LunchTo { get; set; }

        [Range(0.0, 100.0)]
        public float HolidayTime { get; set; }

        [Range(0.0, 100.0)]
        public float LunchTime { get; set; }

        [Range(0.0, 100.0)]
        public float TotalHrsWorked { get; set; }
    }
}