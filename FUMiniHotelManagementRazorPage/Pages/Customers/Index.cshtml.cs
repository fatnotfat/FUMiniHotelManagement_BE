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

namespace FUMiniHotelManagementRazorPage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IHelper _helper;

        public IndexModel(IHelper helper)
        {
            _helper = helper;
        }

        public string? Token { get; set; }

        public IList<Customer> Customers { get; set; } = default!;

        public async Task OnGetAsync(string token)
        {
            Token = token;
            string baseUrl = "http://localhost:5282/api/v1/customers";
            var responseString = await _helper.GetAPI(baseUrl, token);
            Customers = JsonConvert.DeserializeObject<List<Customer>>(responseString)!;
        }
    }
}
