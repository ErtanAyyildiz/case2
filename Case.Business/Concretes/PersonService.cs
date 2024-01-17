using Case.Business.Abstracts;
using Case.DataAccess.Repositories.IRepositories;
using Case.Models;

namespace Case.Business.Concretes
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Person entity)
        {
            _unitOfWork.Person.Add(entity);
            _unitOfWork.Save();
        }

        public Person? GetByID(int id)
        {
            return _unitOfWork.Person.GetByID(id);
        }

        public List<Person> GetList()
        {
            return _unitOfWork.Person.GetList();
        }

        public Person? GetPersonByName(string name)
        {
            return _unitOfWork.Person.GetPersonByName(name);
        }


        public void Remove(Person entity)
        {
            _unitOfWork.Person.Remove(entity);
            _unitOfWork.Save();
        }

        public void Update(Person entity)
        {
            _unitOfWork.Person.Update(entity);
            _unitOfWork.Save();
        }
    }
}
