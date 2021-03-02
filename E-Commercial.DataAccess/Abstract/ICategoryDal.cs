using E_Commercial.Core.DataAccess;
using E_Commercial.Entity.Concrete;

namespace E_Commercial.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}