using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Staff
    {
        public Staff(string email, string password)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            Email = "";
            Password = "";
            if (email.Trim().Equals(configuration.GetSection("StaffAccount:Email").Value))
                Email = email;
            if (password.Trim().Equals(configuration.GetSection("StaffAccount:Password").Value))
                Password = password;

        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
