using BusinessObject;
using Repo.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoomInformationService : IRoomInformationService
    {
        private IRoomInformation _room;

        public RoomInformationService(IRoomInformation room)
        {
            _room = room;
        }

        public void AddNew(RoomInformation room)
        {
            _room.AddNew(room);
        }

        public void Delete(RoomInformation room)
        {
            _room.Delete(room);
        }

        public RoomInformation GetRoomById(int id)
        {
            return _room.GetRoomById(id);
        }

        public List<RoomInformation> GetRooms()
        {
            return _room.GetRooms();

        }

        public IQueryable<RoomInformation> SearchRoom(string name)
        {
            return _room.SearchRoom(name);
        }

        public void UpdateRoom(RoomInformation room)
        {
            _room.UpdateRoom(room);
        }
    }
}
