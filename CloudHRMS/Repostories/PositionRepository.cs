using CloudHRMS.DAO;
using CloudHRMS.Models.DataModels;

namespace CloudHRMS.Repostories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PositionRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public void Create(PositionEntity positionEntity)
        {
            _applicationDbContext.Positions.Add(positionEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            if (null != id)
            {
                var position = _applicationDbContext.Positions.Find(id);
                if (position is not null)
                {
                    _applicationDbContext.Remove(position);
                    _applicationDbContext.SaveChanges();
                }
            }
        }

        public IList<PositionEntity> GetAll()
        {
            return _applicationDbContext.Positions.ToList();
        }

        public PositionEntity GetById(string id)
        {
           return  _applicationDbContext.Positions.Where(w => w.Id == id).SingleOrDefault();
        }

        public void Update(PositionEntity positionEntity)
        {
            _applicationDbContext.Positions.Update(positionEntity);
            _applicationDbContext.SaveChanges();
        }
    }
}
