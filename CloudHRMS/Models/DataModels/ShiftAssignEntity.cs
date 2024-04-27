using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("ShiftAssign")]
    public class ShiftAssignEntity:BaseEntity
    {
        [ForeignKey(nameof(EmployeeId))]
        public string EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; }

        [ForeignKey(nameof(ShiftId))]
        public string ShiftId { get; set; }
        public ShiftEntity Shift { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
