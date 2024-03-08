using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class RoomInformationDAO


    {

        private static RoomInformationDAO instance = null;


        public static RoomInformationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomInformationDAO();
                }
                return instance;
            }


        }

        public List<RoomInformation> GetAllRoomInformation()
        {
            var _context = new FUMiniHotelManagementContext();
            return _context.RoomInformations.Include(c => c.RoomType).ToList();   
        }

        public bool AddNew(RoomInformation roomInformation)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.RoomInformations.SingleOrDefault(c => c.RoomId == roomInformation.RoomId);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.RoomInformations.Add(roomInformation);
                _context.SaveChanges();
                return true;

            }
        }

        public bool Update(RoomInformation roomInformation)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.RoomInformations.SingleOrDefault(c => c.RoomId == roomInformation.RoomId);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(roomInformation);
                _context.SaveChanges();
                return true;
            }
        }

        public RoomInformation GetRoomByID(int id)
        {
            var _context = new FUMiniHotelManagementContext();
            return _context.RoomInformations.Include(c => c.RoomType).SingleOrDefault(a => a.RoomId == id);
        }

        public void DeleteRoom(RoomInformation roomInformation)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.RoomInformations.FirstOrDefault(a => a.RoomId == roomInformation.RoomId);
            _context.RoomInformations.Remove(a);
            _context.SaveChanges();
        }


        public IQueryable<RoomInformation> SearchByName(string searchvalue)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.RoomInformations.Where(a => a.RoomDetailDescription.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

    }
}

