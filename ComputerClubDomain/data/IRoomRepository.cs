using Domain.models;
using System.Collections.Generic;

namespace Domain.data
{
    public interface IRoomRepository
    {
        public void Create(Room hall);

        public List<Room> Halls(List<Room> hall);
    }
}
