using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Services.Interface;
using NuGet.Common;
using Newtonsoft.Json;

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly IHelper _helper;

        public CreateModel(IHelper helper)
        {
            _helper = helper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public string? Token { get; set; }
        public IList<Customer> Customers { get; set; } = default!;


        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string token)
        {
            Token = token;
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            Customers = JsonConvert.DeserializeObject<List<Customer>>(responseString)!;
            if (!ModelState.IsValid || Customers == null || Customer == null)
            {
                return Page();
            }


            var responseString2 = await _helper.PostAPI(baseUrl, token, Customer);
            return RedirectToPage("./Index", new { Token = token });
        }
    }
}
