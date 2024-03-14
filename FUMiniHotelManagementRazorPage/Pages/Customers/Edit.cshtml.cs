using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service.Interface;
using Services.Interface;
using Newtonsoft.Json;
using NuGet.Common;

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly IHelper _helper;

        public EditModel(IHelper helper)
        {
            _helper = helper;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        //public string? ErrorMessageName { get; set; } = default!;
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
            Token = token;
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string token)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                string baseUrl = "http://localhost:5282/api/v1/customers";
                var responseString = await _helper.PutAPI(baseUrl, token, Customer);
                //var status = JsonConvert.DeserializeObject<string>(responseString);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();

            }

            return RedirectToPage("./Index", new { Token = token });
        }
    }
}
