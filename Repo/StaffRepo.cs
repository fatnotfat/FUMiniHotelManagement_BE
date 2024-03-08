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
    public class StaffRepo : IStaffRepo
    {
        public Staff CheckLogin(string email, string password)
        {
            return StaffDAO.Instance.CheckLoginStaff(email, password);
        }
    }
}
