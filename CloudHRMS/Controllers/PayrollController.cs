using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult PayrollProcess(PayrollProcessViewModel ui)
        {
            try
            {
                   List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData = new List<AttendanceMasterCalculatedData>() ;
                    if (ui.DepartmentId != null)
                    {
                    //HR,01-03-2024 to 31-03-2024 
                    List<AttendanceMasterEntity> attendances = _applicationDbContext.AttendanceMasters.Where(w=>w.DepartmentId==ui.DepartmentId &&
                                                                                                                                        (w.AttendanceDate<=ui.ToDate)).OrderBy(o=>o.AttendanceDate)
                                                                                                                                        .ToList();
                    List<AttendanceMasterEntity> distinctEmployees= attendances.DistinctBy(e=>e.EmployeeId).ToList();
                    foreach (AttendanceMasterEntity  distinctEmployee in distinctEmployees)
                    {
                        AttendanceMasterCalculatedData calculatedData = new AttendanceMasterCalculatedData();
                        calculatedData.DepartmentId = distinctEmployee.DepartmentId;
                        calculatedData.EmployeeId= distinctEmployee.EmployeeId;
                        calculatedData.FromDate=ui.FromDate;
                        calculatedData.ToDate=ui.ToDate;
                        calculatedData.BasicPay = _applicationDbContext.Employees.Find(distinctEmployee.EmployeeId).BasicSalary;
                        calculatedData.LateCount= attendances.Where(w=>w.EmployeeId== distinctEmployee.EmployeeId && w.IsLate==true &&(w.AttendanceDate>=ui.FromDate&&w.AttendanceDate<=ui.ToDate)).Count();
                        calculatedData.EarlyOutCount= attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsEarlyOut == true&&(w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.LeaveCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave == true && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.AttendanceDays = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave == false && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        attendanceMasterCalculatedData.Add(calculatedData);
                    }
                    List<PayrollEntity> payrolls = CalculatePayroll(attendanceMasterCalculatedData);
                    _applicationDbContext.Payrolls.AddRange(payrolls);
                    _applicationDbContext.SaveChanges();
                    ViewBag.Info = "successfully save a record to the system";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Info = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }

        private List<PayrollEntity> CalculatePayroll(List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData)
        {
            List<PayrollEntity> payrolls=new List<PayrollEntity>();
            decimal incomeTax = 2000, allowance= 30000,deduction= 10000;
            int workingDays = 30;
            foreach (AttendanceMasterCalculatedData calculatedData in attendanceMasterCalculatedData)
            {
                PayrollEntity payroll=new PayrollEntity();
                payroll.Id = Guid.NewGuid().ToString();
                payroll.CreatedAt=DateTime.Now;
                payroll.FromDate = calculatedData.FromDate;
                payroll.ToDate = calculatedData.ToDate;
                payroll.EmployeeId=calculatedData.EmployeeId;
                payroll.DepartmentId=calculatedData.DepartmentId;
                payroll.IncomeTax=incomeTax;
                payroll.GrossPay=(calculatedData.BasicPay/workingDays)*calculatedData.AttendanceDays+allowance-deduction;
                payroll.NetPay = payroll.GrossPay - payroll.IncomeTax;
                payroll.Allowance=allowance;
                payroll.Deduction=deduction;
                payroll.AttendanceDays=calculatedData.AttendanceDays;
                decimal PayPerDay = (calculatedData.BasicPay / workingDays);
                payroll.AttendanceDeduction = CalculateAttendanceDeductionByAttendancePolicy(calculatedData.FromDate,calculatedData.ToDate,calculatedData.EmployeeId, PayPerDay, calculatedData.LateCount, calculatedData.EarlyOutCount);
                payrolls.Add(payroll);
            }
            return payrolls;
        }

        private decimal CalculateAttendanceDeductionByAttendancePolicy(DateTime fromDate,DateTime toDate,string EmployeeId,decimal PayPerDay, int lateCount, int earlyOutCount)
        {
            decimal attendanceDeduction = 0;
            var attendancePolicy = (from attm in _applicationDbContext.AttendanceMasters
                        join e in _applicationDbContext.Employees
                        on attm.EmployeeId equals e.Id
                        join d in _applicationDbContext.Departments
                        on e.DepartmentId equals d.Id
                        join sa in _applicationDbContext.ShiftAssigns
                        on e.Id equals sa.EmployeeId
                        join s in _applicationDbContext.Shifts
                        on sa.ShiftId equals s.Id
                        join ap in _applicationDbContext.AttendancePolicyEntity
                        on s.AttendancePolicyId equals ap.Id
                        where sa.EmployeeId == EmployeeId && (fromDate >= sa.FromDate && sa.ToDate <=toDate)
                        select ap).Distinct();
            if (null != attendancePolicy)
            {
                if (lateCount > attendancePolicy.First().NumberOfLateTime || attendancePolicy.First().NumberOfEarlyOutTime < earlyOutCount)
                {
                    attendanceDeduction = attendancePolicy.First().DeductionInAmount ?? 0;
                }
                if (attendancePolicy.First().DeductionInDay > 0)
                {
                    attendanceDeduction += (PayPerDay * attendancePolicy.First().DeductionInDay) ?? 0;
                }
            }
            return attendanceDeduction;
        }
    }
}
