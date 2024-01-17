using Case.DataAccess.Abstracts;
using Case.DataAccess.Database;
using Case.DataAccess.MsEntityFrameworks;
using Case.DataAccess.Repositories.IRepositories;

namespace Case.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Person = new PersonDal(_db);
        }

        public IPersonDal Person { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
