using BusinessObject;
using DAO;
using Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class RoomInformationRepo : IRoomInformation
    {
        private RoomInformationDAO dao;

        public RoomInformationRepo()
        {
            dao = new RoomInformationDAO();
        }

        public void AddNew(RoomInformation room)
        {
            dao.AddNew(room);
        }

        public void Delete(RoomInformation room)
        {
            dao.DeleteRoom(room);
        }

        public RoomInformation GetRoomById(int id)
        {
            return dao.GetRoomByID(id);
        }

        public List<RoomInformation> GetRooms()
        {
            return dao.GetAllRoomInformation();
        }

        public IQueryable<RoomInformation> SearchRoom(string name)
        {
            return dao.SearchByName(name);
        }

        public void UpdateRoom(RoomInformation room)
        {
             dao.Update(room);
        }
    }
}
