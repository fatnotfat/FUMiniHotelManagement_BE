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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;

        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public Admin CheckLogin(string email, string password)
        {
            return _adminRepo.CheckLogin(email, password);
        }
    }
}
