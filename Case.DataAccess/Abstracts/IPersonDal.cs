using Case.Models;
using Case.DataAccess.Repositories.IRepositories;

namespace Case.DataAccess.Abstracts
{
    public interface IPersonDal:IRepository<Person>
    {
        public Person? GetPersonByName(string name);

    }
}
