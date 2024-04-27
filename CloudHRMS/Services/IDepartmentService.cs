using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IDepartmentService
    {
        void Create(DepartmentViewModel departmentViewModel);

        IList<DepartmentViewModel> GetAll();

        DepartmentViewModel GetById(string id);

        void Update(DepartmentViewModel departmentViewModel);

        void Delete(string id);
    }
}
