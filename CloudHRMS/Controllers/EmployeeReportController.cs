using CloudHRMS.Models.ViewModels;
using CloudHRMS.Reports;
using CloudHRMS.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CloudHRMS.Controllers
{
    public class EmployeeReportController : Controller
    {
        private readonly IEmployeeReport _employeeReport;

        public EmployeeReportController(IEmployeeReport employeeReport)
        {
            this._employeeReport = employeeReport;
        }

        public IActionResult EmployeeDetailReport() => View();

        public IActionResult ReportByEmployeeFromCodeToCode(string fromCode,string toCode)
        {
            string reportname = $"EmployeeDetails_{Guid.NewGuid():N}.xlsx";
            IList<EmployeeDetailReportViewModel> employees= _employeeReport.EmployeeDetailReport(fromCode, toCode);
            if (employees.Any())
            {
                var exportData = FilesIOHelper.ExporttoExcel<EmployeeDetailReportViewModel>(employees, "employeeDetailReport");
                return File(exportData, "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet", reportname);
            }
            else
            {
                TempData["info"] = "There is no data when report to excel";
                return RedirectToAction("EmployeeDetailReport");
            }
        }
    }
}
