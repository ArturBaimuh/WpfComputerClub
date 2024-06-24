using Domain.data;
using Domain.models;
using System.Collections.Generic;


namespace Interactor
{
    public class ComputerInteractor
    {
        IComputerRepository repository;
        public ComputerInteractor(IComputerRepository repository)
        {
            this.repository = repository;
        }
        public Computer Create(int idHall, string status, string description)
        {
            Computer сomputer = new Computer { ComputerStatus = status, Description = description, IdHall = idHall };
            repository.Create(сomputer);
            return сomputer;
        }
        public List<Computer> Computers(int hall)
        {
            List<Computer> comp = new List<Computer>();
            repository.Computers(comp, hall);
            return comp;
        }
        public Computer Comp(int comp)
        {
            Computer сomputer = new Computer { IdPC = comp };
            return repository.Comp(сomputer);

        }
        public void Rent(int comp, string description)
        {
            Computer сomputer = new Computer { IdPC = comp, Description = description };
            repository.Rent(сomputer);

        }
    }
}
