using BusinessObject;
using Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IStaffService
    {
        public Staff CheckLogin(string email, string password);
    }
}
