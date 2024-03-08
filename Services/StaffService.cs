using BusinessObject;
using Repo.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepo _staffRepo;

        public StaffService(IStaffRepo staffRepo)
        {
            _staffRepo = staffRepo;
        }
        public Staff CheckLogin(string email, string password)
        {
            return _staffRepo.CheckLogin(email, password);
        }
    }
}
