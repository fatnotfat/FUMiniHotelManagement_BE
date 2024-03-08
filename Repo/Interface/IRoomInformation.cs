using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interface
{
    public interface IRoomInformation
    {
        public List<RoomInformation> GetRooms();
        public void AddNew(RoomInformation room);

        public void Delete(RoomInformation room);
        public RoomInformation GetRoomById(int id);

        public void UpdateRoom(RoomInformation room);

        public IQueryable<RoomInformation> SearchRoom(string name);


    }
}
