using BusinessObject;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;

        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }

        public Customer CheckLogin(string email, string password)
        {

            var _context = new FUMiniHotelManagementContext();
            var customer = _context.Customers.FirstOrDefault(a => a.EmailAddress.Equals(email.Trim()) && a.Password.Equals(password.Trim()));
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return null;
            }

        }

        public List<Customer> GetAllCustomer()
        {
            var _context = new FUMiniHotelManagementContext();
            return _context.Customers.ToList();
        }

        public IQueryable<Customer> GetAllCustomerQueryable()
        {
            var _context = new FUMiniHotelManagementContext();
            return _context.Customers;
        }

        public PagedList<Customer> GetCustomers(OwnerParameters ownerParameters)
        {
            return PagedList<Customer>.ToPagedList(GetAllCustomerQueryable()
                                                    .OrderBy(on => on.CustomerFullName),
                                                            ownerParameters.PageNumber,
                                                            ownerParameters.PageSize);
        }


        public bool AddNew(Customer customer)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);

            if (a != null)
            {
                return false;
            }
            else
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;

            }
        }

        public bool Update(Customer customer)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);

            if (a == null)
            {
                return false;
            }
            else
            {
                _context.Entry(a).CurrentValues.SetValues(customer);
                _context.SaveChanges();
                return true;
            }
        }

        public Customer GetCustomerByID(int id)
        {
            var _context = new FUMiniHotelManagementContext();
            return _context.Customers.SingleOrDefault(a => a.CustomerId == id);
        }

        public void DeleteCustomer(Customer customer)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.Customers.FirstOrDefault(a => a.CustomerId == customer.CustomerId);
            if (a != null)
            {
                _context.Customers.Remove(a);
                _context.SaveChanges();
            }
        }

        public IQueryable<Customer> SearchByName(string searchvalue)
        {
            var _context = new FUMiniHotelManagementContext();
            var a = _context.Customers.Where(a => a.CustomerFullName.ToUpper().Contains(searchvalue.Trim().ToUpper()));
            return a;
        }

    }
}
