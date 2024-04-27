using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("DailyAttendance")]
    public class DailyAttendanceEntity:BaseEntity
    {
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set;}
    }
}
