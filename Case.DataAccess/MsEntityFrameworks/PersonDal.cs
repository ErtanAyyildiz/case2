using Case.DataAccess.Abstracts;
using Case.DataAccess.Database;
using Case.DataAccess.Repositories;
using Case.Models;

namespace Case.DataAccess.MsEntityFrameworks
{
    public class PersonDal : Repository<Person>, IPersonDal
    {
        private readonly AppDbContext _db;
        public PersonDal(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public Person? GetPersonByName(string name)
        {
            return _db.People.FirstOrDefault(p => p.Name == name);
        }


    }
}
