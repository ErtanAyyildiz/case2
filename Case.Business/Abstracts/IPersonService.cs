using Case.Models;
using LibraryImplement.Business.Abstracts;

namespace Case.Business.Abstracts
{
    public interface IPersonService : IGenericService<Person>
    {
        public Person? GetPersonByName(string name);
    }
}
