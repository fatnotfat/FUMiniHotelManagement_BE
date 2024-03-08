using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICustomerServices
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
