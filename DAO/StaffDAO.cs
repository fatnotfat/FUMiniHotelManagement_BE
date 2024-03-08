using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StaffDAO
    {
        private static StaffDAO? instance = null;


        public static StaffDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StaffDAO();
                }
                return instance;
            }
        }
        public Staff CheckLoginStaff(string email, string password)
        {
            var staff = new Staff(email, password);
            return staff;
        }
    }
}
