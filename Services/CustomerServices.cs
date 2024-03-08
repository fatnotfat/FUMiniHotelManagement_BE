using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using DAO;
using Repo;
using Repo.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService : ICustomerServices
    {
        private ICustomerRepo _customer;

        public CustomerService(ICustomerRepo customer)
        {
            _customer = customer;

        }

        public void AddNew(Customer customer)
        {
            _customer.AddNew(customer);
        }

        public Customer CheckLogin(string username, string password)
        {
            return _customer.CheckLogin(username, password);
        }

        //public Customer CheckLoginUser(string username, string password)
        //{
        //  return  _customer.CheckLoginUser(username, password);

        //}

        public void Delete(Customer customer)
        {
            _customer.Delete(customer);
        }

        public Customer GetCustomerById(int id)
        {
            return _customer.GetCustomerById(id);
        }

        public List<Customer> GetCustomers()
        {
            return _customer.GetCustomers();
        }

        public IQueryable<Customer> SearchCustomer(string name)
        {
            return _customer.SearchCustomer(name);
        }

        public void UpdateCustomer(Customer customer)
        {
            var customerExisted = GetCustomerById(customer.CustomerId);
            if (customerExisted != null)
            {
                if (customer.CustomerFullName != null)
                {
                    customerExisted.CustomerFullName = customer.CustomerFullName;
                }

                if (customer.Telephone != null)
                {
                    customerExisted.Telephone = customer.Telephone;
                }

                if (customer.CustomerBirthday != null)
                {
                    customerExisted.CustomerBirthday = customer.CustomerBirthday;
                }

                if (customer.Password != null)
                {
                    customerExisted.Password = customer.Password;
                }
                if (customer.CustomerStatus != null)
                {
                    customerExisted.CustomerStatus = customer.CustomerStatus;
                }
                _customer.UpdateCustomer(customer);

            }
        }

        PagedList<Customer> ICustomerServices.GetCustomers(OwnerParameters ownerParameters)
        {
            return _customer.GetCustomers(ownerParameters);
        }
    }
}
