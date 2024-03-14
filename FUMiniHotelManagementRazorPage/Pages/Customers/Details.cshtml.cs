using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Services.Interface;
using Newtonsoft.Json;
using NuGet.Common;

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly IHelper _helper;

        public DetailsModel(IHelper helper)
        {
            _helper = helper;
        }

        public string? Token { get; set; }
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, string token)
        {
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            if (id == null || customers == null)
            {
                return NotFound();
            }

            Token = token;

            string baseUrl1 = $"http://localhost:5282/api/v1/customers/{id}";
            var responseString1 = await _helper.GetAPI(baseUrl1, token);
            var customer = JsonConvert.DeserializeObject<Customer>(responseString1);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
