using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels
{
    [Table("Payroll")]
    public class PayrollEntity:BaseEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; }
        [ForeignKey("DepartmentId")]
        public string DepartmentId { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
    }
}
