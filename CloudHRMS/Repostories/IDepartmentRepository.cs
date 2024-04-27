using CloudHRMS.Models.DataModels;

namespace CloudHRMS.Repostories
{
    public interface IDepartmentRepository
    {
        void Create(DepartmentEntity departmentEntity);

        IList<DepartmentEntity> GetAll();

        DepartmentEntity GetById(string id);

        void Update(DepartmentEntity departmentEntity);

        void Delete(string id);
    }
}
