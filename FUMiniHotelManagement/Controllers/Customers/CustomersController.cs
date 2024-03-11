using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service.Interface;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;
using BusinessObject.DTO;
using BusinessObject.Helpers;
using Newtonsoft.Json;

namespace Ass1.Controllers.Customers
{

    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerServices _service;


        public CustomersController(ICustomerServices service)
        {
            _service = service;
        }

        // GET: api/Customers
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            if (_service.GetCustomers() == null)
            {
                return NotFound();
            }
            return Ok(_service.GetCustomers().ToList());
        }

        // GET: api/Customers/5
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            if (_service.GetCustomers() == null)
            {
                return NotFound();
            }
            var customer = _service.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet]
        [Route("search-by-name/{name}")]
        public ActionResult<List<Customer>> GetCustomerByName(string name)
        {
            if (_service.GetCustomers() == null)
            {
                return NotFound();
            }
            var customers = _service.SearchCustomer(name);

            return Ok(customers.ToList());
        }


        [Authorize(Roles = "Admin")]
        [EnableQuery]
        [HttpGet]
        [Route("search-by-name/paging")]
        public ActionResult<IEnumerable<Customer>> GetCustomerByNamePaging([FromQuery] OwnerParameters ownerParameters)
        {
            if (_service.GetCustomers() == null)
            {
                return NotFound();
            }
            var customers = _service.GetCustomers(ownerParameters);
            var metadata = new
            {
                customers.TotalCount,
                customers.PageSize,
                customers.CurrentPage,
                customers.TotalPages,
                customers.HasNext,
                customers.HasPrevious
            };

            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(customers);
        }



        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Staff")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult PutCustomer([FromBody] Customer customer)
        {
            var customerExisted = _service.GetCustomerById(customer.CustomerId);
            if (customerExisted == null)
            {
                return BadRequest("Id not found!");
            }

            try
            {
                if (customer != null)
                {
                    _service.UpdateCustomer(customer);
                }
                else
                {
                    return BadRequest("Customer object is null!");
                }
            }
            catch (Exception)
            {
                if (_service.GetCustomerById(customer.CustomerId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update successfully!");
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            if (_service.GetCustomers() == null)
            {
                return Problem("Entity set 'FUMiniHotelManagementContext.Customers'  is null.");
            }
            _service.AddNew(customer);
            return Ok();
        }

        // DELETE: api/Customers/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (_service.GetCustomers() == null)
            {
                return NotFound();
            }
            var customer = _service.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _service.Delete(customer);

            return Ok();
        }


    }
}
