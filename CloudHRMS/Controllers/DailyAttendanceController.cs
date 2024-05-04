using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace CloudHRMS.Controllers
{
    public class DailyAttendanceController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DailyAttendanceController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IActionResult List() //Show
        {
            IList<DailyAttendanceViewModel> dailyAttendances = (from d in _applicationDbContext.DailyAttendances
                        join e in _applicationDbContext.Employees
                        on d.EmployeeId equals e.Id
                        join dept in _applicationDbContext.Departments
                        on e.DepartmentId equals dept.Id select new DailyAttendanceViewModel
                        {
                            Id = d.Id,
                            AttendanceDate = d.AttendanceDate,
                            InTime = d.InTime,
                            OutTime = d.OutTime,
                            EmployeeId =e.Code +" "+e.Name,
                            DepartmentId =dept.Code +" " +dept.Name,
                        }).OrderBy(o=>o.AttendanceDate).ThenBy(e=>e.EmployeeId).ToList();
            return View(dailyAttendances);
        }

        public IActionResult Edit(string id)
        {
            if (id != null)
            {
                DepartmentViewModel organization = _applicationDbContext.Departments.Where(x => x.Id == id).Select(s => new DepartmentViewModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
                    ExtensionPhone = s.ExtensionPhone
                }).SingleOrDefault();
                return View(organization);
            }
            else
            {
                return RedirectToAction("List");
            }
        }
        public IActionResult Delete(string id)
        {
            try
            {
                var organization = _applicationDbContext.Departments.Find(id);
                if (organization is not null)
                {
                    _applicationDbContext.Remove(organization);
                    _applicationDbContext.SaveChanges();
                }
                TempData["Info"] = "Save Successfully";
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error Occur When Deleting";
            }

            return RedirectToAction("List");
        }
        public IActionResult Entry()
        {
            var departments = _applicationDbContext.Departments.Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Departments = departments;
            var employees = _applicationDbContext.Employees.Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                Code = s.Code + "/" +s.Name
            }).OrderBy(o => o.Code).ToList();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        public IActionResult Entry(DailyAttendanceViewModel ui)
        {
            try
            {
                string departmentId = ui.DepartmentId;
                if ("x".Equals(ui.DepartmentId))
                {
                    departmentId = _applicationDbContext.Employees.Where(e => e.Id == ui.EmployeeId).FirstOrDefault().DepartmentId;
                }
                //Data exchange from view model to data model
                var dailyAttendance = new DailyAttendanceEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    AttendanceDate = ui.AttendanceDate,
                    InTime = ui.InTime,
                    OutTime = ui.OutTime,
                    EmployeeId = ui.EmployeeId,
                    DepartmentId = departmentId,
                };
                _applicationDbContext.DailyAttendances.Add(dailyAttendance);
                _applicationDbContext.SaveChanges();
                TempData["Info"] = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }
    }
}
