using Case.DataAccess.Repositories.IRepositories;
using Case.Models;

namespace Case.DataAccess.Abstracts
{
    public interface IPersonDal : IRepository<Person>
    {
        public Person? GetPersonByName(string name);

    }
}
