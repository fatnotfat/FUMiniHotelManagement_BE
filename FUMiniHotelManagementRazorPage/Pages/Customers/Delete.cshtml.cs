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

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly IHelper _helper;

        public DeleteModel(IHelper helper)
        {
            _helper = helper;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public string? Token { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string token)
        {
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            if (id == null || customers == null)
            {
                return NotFound();
            }

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
            Token = token;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string token)
        {
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(responseString);
            if (id == null || customers == null)
            {
                return NotFound();
            }
            string baseUrl1 = $"http://localhost:5282/api/v1/customers/{id}";
            var responseString1 = await _helper.GetAPI(baseUrl1, token);
            var customer = JsonConvert.DeserializeObject<Customer>(responseString1);

            if (customer != null)
            {
                Customer = customer;
                var responseString2 = await _helper.DeleteAPI(baseUrl1, token);
            }

            return RedirectToPage("./Index", new { Token = token });
        }
    }
}
