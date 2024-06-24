using DataBaseComputerClub;
using Domain.models;
using Interactor;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace AspComputerClub.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {

        DBmonitoring context;

        RoomInteractor interactor;
        public RoomController(DBmonitoring context, RoomInteractor interactor)
        {
            this.interactor = interactor;
            this.context = context;
        }
        [HttpGet("Create")]
        public Room Create(bool vip)
        {
            return interactor.Create(vip);
        }

        [HttpGet("Halls")]
        public List<Room> Halls()
        {
            return interactor.Halls();
        }
    }
}
