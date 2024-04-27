using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("AttendanceMaster")]
    public class AttendanceMasterEntity:BaseEntity
    {
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public string EmployeeId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public string DepartmentId { get; set; }
        [ForeignKey(nameof(ShiftId))]
        public string ShiftId { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }
    }
}
