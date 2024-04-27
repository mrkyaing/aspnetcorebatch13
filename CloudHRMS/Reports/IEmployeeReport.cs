using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Reports
{
    public interface IEmployeeReport
    {
        IList<EmployeeDetailReportViewModel> EmployeeDetailReport(string fromCode,string toCode);
    }
}
