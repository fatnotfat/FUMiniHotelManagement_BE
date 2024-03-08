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
    public class AdminRepo : IAdminRepo
    {
        public Admin CheckLogin(string email, string password)
        {
            return AdminDAO.Instance.CheckLoginAdmin(email, password);
        }
    }
}
