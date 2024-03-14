using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Newtonsoft.Json;
using Services.Interface;
using FUMiniHotelManagementRazorPage.Pagging;
using System.Configuration;

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IHelper _helper;
        private readonly IConfiguration _configuration;


        public IndexModel(IHelper helper, IConfiguration configuration)
        {
            _helper = helper;
            _configuration = configuration;
        }

        public string? Token { get; set; }

        //public IList<Customer> Customers { get; set; } = default!;

        public PaginatedList<Customer> Customers { get; set; } = default!;



        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string token, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            if (currentFilter != null)
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;


            Token = token;
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            //Customers = JsonConvert.DeserializeObject<List<Customer>>(responseString)!;

            List<Customer> customerList = JsonConvert.DeserializeObject<List<Customer>>(responseString)!;
            //if (customerList != null && customerList.Count > 0)
            //{
            if(customerList == null)
            {
                customerList = new List<Customer>();
            }
            IQueryable<Customer> customerIQ = customerList.AsQueryable();
            //}



            if (!String.IsNullOrEmpty(searchString))
            {
                customerIQ = customerIQ.Where(s => s.CustomerFullName!.ToUpper().Contains(searchString.Trim().ToUpper()));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.CustomerFullName!);
                    break;
                case "Date":
                    customerIQ = customerIQ.OrderBy(s => s.CustomerBirthday);
                    break;
                case "date_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.CustomerBirthday);
                    break;
                default:
                    customerIQ = customerIQ.OrderBy(s => s.CustomerFullName!);
                    break;
            }

            var pageSize = _configuration.GetValue("PageSize", 4);
            Customers = PaginatedList<Customer>.Create(customerIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
