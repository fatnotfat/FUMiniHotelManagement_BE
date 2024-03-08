using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AdminDAO
    {
        private static AdminDAO? instance = null;


        public static AdminDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdminDAO();
                }
                return instance;
            }
        }

        public Admin CheckLoginAdmin(string email, string password)
        {
            var admin = new Admin(email, password);
            return admin;
        }
    }
}
