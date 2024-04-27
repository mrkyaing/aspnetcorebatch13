using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class AttendanceMasterController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public AttendanceMasterController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        public IActionResult List()
        {
            //mapper.map<source,destination>(dataList);
            return View(_mapper.Map<List<AttendanceMasterEntity>,List<AttendanceMasterViewModel>>(_applicationDbContext.AttendanceMasters.ToList()));
        }
        public IActionResult DayEndProcess()
        {
            ViewBag.Shifts = _mapper.Map<List<ShiftEntity>, List<ShiftViewModel>>(_applicationDbContext.Shifts.ToList());
            ViewBag.Employees = _mapper.Map<List<EmployeeEntity>, List<EmployeeViewModel>>(_applicationDbContext.Employees.ToList());
            ViewBag.Departments = _mapper.Map<List<DepartmentEntity>, List<DepartmentViewModel>>(_applicationDbContext.Departments.ToList());
            return View();
        }
        [HttpPost]
        public IActionResult DayEndProcess(AttendanceMasterViewModel ui)
        {
            try
            {
                //Data exchange from view model to data model by using automapper 
                List<AttendanceMasterEntity> attendanceMasters = new List<AttendanceMasterEntity>();         
                var DailyAttendancesWithShiftAssignsData = (from d in _applicationDbContext.DailyAttendances
                            join sa in _applicationDbContext.ShiftAssigns
                            on d.EmployeeId equals sa.EmployeeId
                            where sa.EmployeeId == ui.EmployeeId &&
                                        (ui.AttendanceDate >= sa.FromDate && sa.FromDate <= ui.ToDate)
                            select new
                            {
                                dailyAttendance=d,
                                shiftAssign=sa
                            }).ToList();             
                foreach(var data in DailyAttendancesWithShiftAssignsData)
                {
                    ShiftEntity definedShift = _applicationDbContext.Shifts.Where(s => s.Id == data.shiftAssign.ShiftId).SingleOrDefault() ;
                    if(definedShift is not null)
                    {
                        AttendanceMasterEntity attendanceMaster = new AttendanceMasterEntity();
                        attendanceMaster.Id = Guid.NewGuid().ToString();
                        attendanceMaster.CreatedAt = DateTime.Now;
                        attendanceMaster.IsLeave = false;
                        attendanceMaster.InTime = data.dailyAttendance.InTime;
                        attendanceMaster.OutTime = data.dailyAttendance.OutTime;
                        attendanceMaster.EmployeeId = data.shiftAssign.EmployeeId;
                        attendanceMaster.ShiftId = definedShift.Id;
                        attendanceMaster.DepartmentId = data.dailyAttendance.DepartmentId;
                        attendanceMaster.AttendanceDate = data.dailyAttendance.AttendanceDate;
                        //checking out the late status 
                        if (data.dailyAttendance.InTime> definedShift.LateAfter)
                        {
                            attendanceMaster.IsLate = true;
                        }
                        else
                        {
                            attendanceMaster.IsLate = false;
                        }
                        //checking out the late status 
                        if (data.dailyAttendance.OutTime < definedShift.EarlyOutBefore)
                        {
                            attendanceMaster.IsEarlyOut = true;
                        }
                        else
                        {
                            attendanceMaster.IsEarlyOut = false;
                        }
                        attendanceMasters.Add(attendanceMaster);
                    }//end of the deifned shift not null 
                
                }
                _applicationDbContext.AttendanceMasters.AddRange(attendanceMasters);
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
