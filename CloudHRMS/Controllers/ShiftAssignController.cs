using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ShiftAssignController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ShiftAssignController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        public IActionResult List()
        {
            //mapper.map<source,destination>(dataList);
            return View(_mapper.Map<List<ShiftAssignEntity>,List<ShiftAssignViewModel>>(_applicationDbContext.ShiftAssigns.ToList()));
        }
        public IActionResult Entry()
        {
            ViewBag.Shifts = _mapper.Map<List<ShiftEntity>, List<ShiftViewModel>>(_applicationDbContext.Shifts.ToList());
            ViewBag.Employees = _mapper.Map<List<EmployeeEntity>, List<EmployeeViewModel>>(_applicationDbContext.Employees.ToList());
            return View();
        }
        [HttpPost]
        public IActionResult Entry(ShiftAssignViewModel ui)
        {
            try
            {
                //Data exchange from view model to data model by using automapper 
                ShiftAssignEntity shiftAssignEntity = _mapper.Map<ShiftAssignEntity>(ui);
                shiftAssignEntity.Id = Guid.NewGuid().ToString();
                shiftAssignEntity.CreatedAt = DateTime.Now;
                _applicationDbContext.ShiftAssigns.Add(shiftAssignEntity);
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
