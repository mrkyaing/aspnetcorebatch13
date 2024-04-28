namespace CloudHRMS.Models.DataModels
{
    public class AttendanceMasterCalculatedData
    {
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int LateCount { get; set; }
        public int EarlyOutCount { get; set; }
        public int LeaveCount { get; set; }
    }
}
