using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("Shift")]
    public class ShiftEntity : BaseEntity
    {
        public string Name { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public TimeSpan LateAfter { get; set; }
        public TimeSpan EarlyOutBefore { get; set; }
        
        [ForeignKey(nameof(AttendancePolicyId))]
        public string AttendancePolicyId { get; set; }
        public AttendancePolicyEntity  AttendancePolicy   { get; set; }
    }
}
