using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public PayrollController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        public IActionResult List()
        {
            //mapper.map<source,destination>(dataList);
            return View(_mapper.Map<List<PayrollEntity>,List<PayrollViewModel>>(_applicationDbContext.Payrolls.ToList()));
        }
        public IActionResult PayrollProcess()
        {
            ViewBag.Employees = _mapper.Map<List<EmployeeEntity>, List<EmployeeViewModel>>(_applicationDbContext.Employees.ToList());
            ViewBag.Departments = _mapper.Map<List<DepartmentEntity>, List<DepartmentViewModel>>(_applicationDbContext.Departments.ToList());
            return View();
        }
        [HttpPost]
        public IActionResult PayrollProcess(PayrollViewModel ui)
        {
            try
            {
                //Data exchange from view model to data model by using automapper 
                { 
                  
                }
              //  _applicationDbContext.AttendanceMasters.AddRange(attendanceMasters);
                _applicationDbContext.SaveChanges();
                ViewBag.Info = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                ViewBag.Info = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }
    }
}
