using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repostories;

namespace CloudHRMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        public void Create(DepartmentViewModel departmentViewModel)
        {
            try
            {
                var IsDepartmentCodeAlreadyExists = _departmentRepository.GetAll().Where(w => w.Code == departmentViewModel.Code).Any();
                if (IsDepartmentCodeAlreadyExists)
                {
                    throw new Exception("Code already exists in the system.");
                }
                //Data exchange from view model to data model
                var department = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = departmentViewModel.Code,
                    Name = departmentViewModel.Name,
                    ExtensionPhone = departmentViewModel.ExtensionPhone,
                };
                _departmentRepository.Create(department);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(string id)
        {
            _departmentRepository.Delete(id);
        }

        public IList<DepartmentViewModel> GetAll()
        {
            return _departmentRepository.GetAll().Select(
                 s => new DepartmentViewModel
                 {
                     Id = s.Id,
                     Code = s.Code,
                     Name = s.Name,
                     ExtensionPhone = s.ExtensionPhone,
                 }).ToList();
        }

        public DepartmentViewModel GetById(string id)
        {
            var departmentEntity = _departmentRepository.GetById(id);
            return new DepartmentViewModel()
            {
                Id = departmentEntity.Id,
                Code = departmentEntity.Code,
                Name = departmentEntity.Name,
                ExtensionPhone = departmentEntity.ExtensionPhone,
            };
        }

        public void Update(DepartmentViewModel departmentViewModel)
        {
            var department = new DepartmentEntity()
            {
                Id = departmentViewModel.Id,
                Code = departmentViewModel.Code,
                Name = departmentViewModel.Name,
                ExtensionPhone = departmentViewModel.ExtensionPhone,
            };
            _departmentRepository.Update(department);
        }
    }
}