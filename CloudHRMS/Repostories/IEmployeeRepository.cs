using CloudHRMS.Models.DataModels;
namespace CloudHRMS.Repostories
{
    public interface IEmployeeRepository
    {
        void Create(EmployeeEntity entity);
        IList<EmployeeEntity> GetAll();
        EmployeeEntity GetById(string id);
        void Update(EmployeeEntity entity);
        void Delete(string id);
    }
}
