using CloudHRMS.Models.ViewModels;


namespace CloudHRMS.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(EmployeeViewModel viewModel,string profileUrl);
        IList<EmployeeViewModel> GetAll();
        EmployeeViewModel GetById(string id);
        void Update(EmployeeViewModel viewModel);
        void Delete(string id);
       // IList<EmployeeDetail> GetByFromCodeToCodeDepartentId(string fromCode, string toCode, string DeprtmentId);
    }
}
