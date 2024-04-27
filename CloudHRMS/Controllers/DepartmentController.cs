using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace CloudHRMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List() //Show
        {
            IList<DepartmentViewModel> Departments = _applicationDbContext.Departments.Select(
                 s => new DepartmentViewModel
                 {
                     Id = s.Id,
                     Code = s.Code,
                     Name = s.Name,
                     ExtensionPhone = s.ExtensionPhone,
                     TotalEmployeeCount = _applicationDbContext.Employees.Where(e => e.DepartmentId == s.Id).Count()
                 }).ToList();
            return View(Departments);
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
        public IActionResult Entry() => View();

        [HttpPost]
        public IActionResult Entry(DepartmentViewModel ui)
        {
            try
            {
                //Data exchange from view model to data model
                var organization = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = ui.Code,
                    Name = ui.Name,
                    ExtensionPhone = ui.ExtensionPhone
                };
                _applicationDbContext.Departments.Add(organization);
                _applicationDbContext.SaveChanges();
                ViewBag.Info = "successfully save a record to the system";
            }
            catch (Exception ex)
            {
                ViewBag.Info = "Error occur when  saving a record  to the system";
            }
            return View();
        }
    }
}
