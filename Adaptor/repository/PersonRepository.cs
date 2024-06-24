using DataBaseComputerClub;
using Domain.data;
using Domain.models;

namespace Adaptor.repository
{
    public class PersonRepository : IPersonRepository
    {
        private DBmonitoring context;
        public PersonRepository(DBmonitoring context)
        {
            this.context = context;
        }
        public bool Create(Person p)
        {
            bool a = false;
            foreach (var i in context.Persons)
            {
                if (i.Login == p.Login)
                {
                    a = true;
                }
            }
            if (a == false)
            {
                context.Add(p);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Authorization(Person p)
        {
            foreach (var i in context.Persons)
            {
                if ((i.Login == p.Login) && (i.Paswword == p.Paswword))
                {
                    return i.IdClient;
                }
            }
            return -1;
        }
        public bool Work(Person p)
        {
            foreach (var i in context.Persons)
            {
                if (i.IdClient == p.IdClient)
                {
                    return i.Worker;
                }
            }
            return false;
        }
        public Person NamePerson(Person p)
        {
            foreach (var i in context.Persons)
            {
                if (i.IdClient == p.IdClient)
                {
                    p.Name = i.Name;
                    p.Surname = i.Surname;
                }
            }
            return p;
        }
        public int Switch(Person p)
        {
            foreach (var i in context.Persons)
            {
                if (i.IdClient == p.IdClient)
                {
                    return i.Comp;
                }
            }
            return 000;
        }
    }
}
