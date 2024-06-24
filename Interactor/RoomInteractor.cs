using Domain.data;
using Domain.models;
using System.Collections.Generic;


namespace Interactor
{
    public class RoomInteractor
    {
        IRoomRepository repository;
        public RoomInteractor(IRoomRepository repository)
        {
            this.repository = repository;
        }
        public Room Create(bool vip)
        {
            Room hall = new Room { Vip = vip };
            repository.Create(hall);
            return hall;
        }
        public List<Room> Halls()
        {
            List<Room> hall = new List<Room>();
            repository.Halls(hall);
            return hall;
        }
    }
}
