using Domain.models;

namespace Domain.data
{
    public interface IPersonRepository
    {
        public bool Create(Person p);
        public int Authorization(Person p);
        public int Switch(Person p);
        public bool Work(Person p);
        public Person NamePerson(Person p);
    }
}
