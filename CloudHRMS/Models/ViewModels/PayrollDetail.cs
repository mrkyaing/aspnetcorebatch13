namespace CloudHRMS.Models.ViewModels
{
    public class PayrollDetail
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string EmployeeInfo { get; set; }
        public decimal BasicSalary { get; set; }
        public string DepartmentInfo { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal AttendanceDays { get; set; }
        public decimal AttendanceDeduction { get; set; }
        public decimal IncomeTax { get; set; }
    }
}
