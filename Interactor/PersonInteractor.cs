using Domain.data;
using Domain.models;

namespace Interactor
{
    public class PersonInteractor
    {
        IPersonRepository repository;
        public PersonInteractor(IPersonRepository repository)
        {
            this.repository = repository;
        }
        public bool Create(string name, string surname, string patronymic, string login, string paswword, int comp, bool work)
        {
            Person p = new Person { Name = name, Surname = surname, Patronymic = patronymic, Login = login, Paswword = paswword, Comp = comp, Worker = work };
            return repository.Create(p);
        }

        public int Authorization(string login, string password)

        {
            Person p = new Person { Login = login, Paswword = password };
            int a = repository.Authorization(p);
            return a;
        }
        public bool Work(int idClient)

        {
            Person p = new Person { IdClient = idClient };
            bool a = repository.Work(p);
            return a;
        }
        public Person NamePerson(int idClient)

        {
            Person p = new Person { IdClient = idClient };
            Person a = repository.NamePerson(p);
            return a;
        }
        public int Switch(int idClient)

        {
            Person p = new Person { IdClient = idClient };
            int a = repository.Switch(p);
            return a;
        }
    }
}
