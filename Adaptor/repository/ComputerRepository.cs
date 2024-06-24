using DataBaseComputerClub;
using Domain.data;
using Domain.models;
using System.Collections.Generic;
using System.Linq;

namespace Adaptor.repository
{
    public class ComputerRepository : IComputerRepository
    {
        private DBmonitoring context;
        public ComputerRepository(DBmonitoring context)
        {
            this.context = context;
        }
        public void Create(Computer comp)
        {
            context.Add(comp);
            context.SaveChanges();
        }
        public List<Computer> Computers(List<Computer> comp, int hall)
        {
            foreach (var i in context.Computers)
            {
                if (i.IdHall == hall)
                    comp.Add(i);
            }
            return comp;
        }
        public Computer Comp(Computer comp)
        {
            var tempdata = context.Computers.FirstOrDefault(x => x.IdPC == comp.IdPC);
            return tempdata;
        }
        public void Rent(Computer comp)
        {
            if (comp.Description == "удалить")
            {
                foreach (var i in context.Computers)
                {
                    if (i.IdPC == comp.IdPC)
                    {
                        i.Description = "";
                    }
                }
            }
            else
                foreach (var i in context.Computers)
                {

                    if (comp.ComputerStatus == "не работает")
                    {
                        if (i.IdPC == comp.IdPC)
                        {
                            i.ComputerStatus = "работает";
                            i.Description += comp.Description;
                        }
                    }
                    else
                    {
                        if (i.IdPC == comp.IdPC)
                        {
                            i.ComputerStatus = "не работает";
                            i.Description += comp.Description;
                        }
                    }
                }
            context.SaveChanges();
        }
    }
}
