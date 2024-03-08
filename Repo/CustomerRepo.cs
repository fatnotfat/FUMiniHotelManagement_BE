using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using DAO;
using Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private CustomerDAO dao;
        public CustomerRepo()
        {
            dao = new CustomerDAO();
        }
        public void AddNew(Customer customer)
        {
            dao.AddNew(customer);
        }

        public Customer CheckLogin(string username, string password)
        {
            return dao.CheckLogin(username, password);
        }

        //public Customer CheckLoginUser(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(Customer customer)
        {
            dao.DeleteCustomer(customer);
        }

        public Customer GetCustomerById(int id)
        {
            return dao.GetCustomerByID(id);
        }

        public List<Customer> GetCustomers()
        {
            return dao.GetAllCustomer();
        }

        public PagedList<Customer> GetCustomers(OwnerParameters ownerParameters)
        {
            return dao.GetCustomers(ownerParameters);
        }

        public IQueryable<Customer> SearchCustomer(string name)
        {
            return dao.SearchByName(name);
        }

        public void UpdateCustomer(Customer customer)
        {
            dao.Update(customer);
        }
    }
}
