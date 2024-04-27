using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ShiftController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        public IActionResult List()
        {
            //mapper.map<source,destination>(dataList);
            return View(_mapper.Map<List<ShiftEntity>,List<ShiftViewModel>>(_applicationDbContext.Shifts.ToList()));
        }
        public IActionResult Entry()
        {
            ViewBag.AttedancePolicies = _mapper.Map<List<AttendancePolicyEntity>, List<AttendancePolicyViewModel>>(_applicationDbContext.AttendancePolicyEntity.ToList());
            return View();
        }
        [HttpPost]
        public IActionResult Entry(ShiftViewModel ui)
        {
            try
            {
                //Data exchange from view model to data model by using automapper 
                ShiftEntity shiftEntity = _mapper.Map<ShiftEntity>(ui);
                shiftEntity.Id = Guid.NewGuid().ToString();
                shiftEntity.CreatedAt = DateTime.Now;
                _applicationDbContext.Shifts.Add(shiftEntity);
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
