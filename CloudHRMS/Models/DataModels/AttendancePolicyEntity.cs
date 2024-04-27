using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("AttendancePolicyEntity")]
    public class AttendancePolicyEntity:BaseEntity
    {
        public string Name { get; set; }
        public int NumberOfLateTime { get; set; }
        public int NumberOfEarlyOutTime { get; set; }
        public decimal? DeductionInAmount { get; set; }
        public int? DeductionInDay { get; set; }
    }
}
