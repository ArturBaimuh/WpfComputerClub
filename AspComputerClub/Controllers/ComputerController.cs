using DataBaseComputerClub;
using Domain.models;
using Interactor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspComputerClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputerController : Controller
    {

        DBmonitoring context;

        ComputerInteractor interactor;
        public ComputerController(DBmonitoring context, ComputerInteractor interactor)
        {
            this.interactor = interactor;
            this.context = context;
        }
        [HttpGet("Create")]
        public Computer Create(int hall, string status, string description)
        {
            return interactor.Create(hall, status, description);
        }
        [HttpGet("Computers")]
        public List<Computer> Computers(int hall)
        {
            return interactor.Computers(hall);
        }
        [HttpGet("Comp")]
        public Computer Comp(int comp)
        {
            var a = interactor.Comp(comp);
            return interactor.Comp(comp);
        }
        [HttpGet("Rent")]

        public void Rent(int comp, string description)
        {
            interactor.Rent(comp, description);
        }


    }
}
