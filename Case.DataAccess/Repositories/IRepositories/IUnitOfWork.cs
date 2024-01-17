using Case.DataAccess.Abstracts;

namespace Case.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IPersonDal Person { get; }
        void Save();
    }
}
