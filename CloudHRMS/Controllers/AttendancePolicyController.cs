using AutoMapper;
using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
namespace CloudHRMS.Controllers
{
    public class AttendancePolicyController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public AttendancePolicyController(ApplicationDbContext applicationDbContext,IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        public IActionResult List() //Show
        {
            List<AttendancePolicyViewModel> attendancePolicies = _mapper.Map<List<AttendancePolicyEntity>, List<AttendancePolicyViewModel>>(_applicationDbContext.AttendancePolicyEntity.ToList());
            return View(attendancePolicies);
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
            return View();
        }

        [HttpPost]
        public IActionResult Entry(AttendancePolicyViewModel ui)
        {
            try
            {             
                //Data exchange from view model to data model by using automapper 
                AttendancePolicyEntity attendancePolicyEntity=_mapper.Map<AttendancePolicyEntity>(ui);
                attendancePolicyEntity.Id = Guid.NewGuid().ToString();
                attendancePolicyEntity.CreatedAt = DateTime.Now;
                _applicationDbContext.AttendancePolicyEntity.Add(attendancePolicyEntity);
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
