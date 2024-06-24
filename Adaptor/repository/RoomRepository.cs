using DataBaseComputerClub;
using Domain.data;
using Domain.models;
using System.Collections.Generic;

namespace Adaptor.repository
{
    public class RoomRepository : IRoomRepository
    {
        private DBmonitoring context;
        public RoomRepository(DBmonitoring context)
        {
            this.context = context;
        }
        public void Create(Room hall)
        {
            context.Add(hall);
            context.SaveChanges();
        }
        public List<Room> Halls(List<Room> hall)
        {
            foreach (var i in context.Rooms)
            {
                hall.Add(i);
            }
            return hall;
        }
    }
}
