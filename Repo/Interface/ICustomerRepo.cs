using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Interface
{
    public interface ICustomerRepo
    {
        public Customer CheckLogin(string username, string password);

        //public Customer CheckLoginUser(string username, string password);
        public PagedList<Customer> GetCustomers(OwnerParameters ownerParameters);
        public List<Customer> GetCustomers();
        public void AddNew(Customer customer);

        public void Delete(Customer customer);
        public Customer GetCustomerById(int id);

        public void UpdateCustomer(Customer customer);

        public IQueryable<Customer> SearchCustomer(string name);
    }
}
