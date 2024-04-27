using CloudHRMS.DAO;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Reports
{
    public class EmployeeDetailReport : IEmployeeReport
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeDetailReport(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        IList<EmployeeDetailReportViewModel> IEmployeeReport.EmployeeDetailReport(string fromCode, string toCode)
        {
            IList<EmployeeDetailReportViewModel> employees = (from e in _applicationDbContext.Employees
                                                                                          join d in _applicationDbContext.Departments
                                                                                          on e.DepartmentId equals d.Id
                                                                                          join p in _applicationDbContext.Positions
                                                                                          on e.PositionId equals p.Id
                                                                                          where e.Code.CompareTo(fromCode) >= 0 && e.Code.CompareTo(toCode) <= 0
                                                  select new EmployeeDetailReportViewModel
                                                  {
                                                      Name = e.Name,
                                                      Email = e.Email,
                                                      DOB = e.DOB.ToString("yyyy-MM-dd"),
                                                      BasicSalary = e.BasicSalary,
                                                      Address = e.Address,
                                                      Gender = e.Gender,
                                                      Phone = e.Phone,
                                                      Code = e.Code,
                                                      DOE = e.DOE.ToString("yyyy-MM-dd"),
                                                      DepartmentInfo = d.Code + "/" + d.Name,
                                                      PositionInfo = p.Code + "/" + p.Name,
                                                  }).ToList();
            return employees;
        }
    }
}
