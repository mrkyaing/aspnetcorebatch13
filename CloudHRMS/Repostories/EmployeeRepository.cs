using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;

namespace CloudHRMS.Repostories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public void Create(EmployeeEntity entity)
        {
            _applicationDbContext.Employees.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            if (null != id)
            {
                var entity = _applicationDbContext.Employees.Find(id);
                if (entity is not null)
                {
                    _applicationDbContext.Remove(entity);
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        public IList<EmployeeEntity> GetAll()
        {
            return _applicationDbContext.Employees.ToList();
        }

        public EmployeeEntity GetById(string id)
        {
           return  _applicationDbContext.Employees.Where(w => w.Id == id).SingleOrDefault();
        }

        public void Update(EmployeeEntity entity)
        {
            _applicationDbContext.Employees.Update(entity);
            _applicationDbContext.SaveChanges();
        }
    }
}
