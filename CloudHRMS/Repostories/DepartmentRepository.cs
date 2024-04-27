using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;

namespace CloudHRMS.Repostories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public void Create(DepartmentEntity departmentEntity)
        {
            _applicationDbContext.Departments.Add(departmentEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            if (null != id)
            {
                var department = _applicationDbContext.Departments.Find(id);
                if (department is not null)
                {
                    _applicationDbContext.Remove(department);
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        public IList<DepartmentEntity> GetAll()
        {
            return _applicationDbContext.Departments.ToList();
        }

        public DepartmentEntity GetById(string id)
        {
            return _applicationDbContext.Departments.Where(w => w.Id == id).SingleOrDefault();
        }

        public void Update(DepartmentEntity departmentEntity)
        {
            _applicationDbContext.Departments.Update(departmentEntity);
            _applicationDbContext.SaveChanges();
        }
    }
}