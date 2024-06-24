using Domain.models;
using System.Collections.Generic;

namespace Domain.data
{
    public interface IComputerRepository
    {
        public void Create(Computer comp);
        public List<Computer> Computers(List<Computer> comp, int hall);
        public Computer Comp(Computer comp);
        public void Rent(Computer comp);
    }
}
