using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_web_apps_with_EF_Core.Data;
using ASP.NET_Core_web_apps_with_EF_Core.Models;

namespace ASP.NET_Core_web_apps_with_EF_Core.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ASP.NET_Core_web_apps_with_EF_Core.Data.ContosoPizzaContext _context;

        public DetailsModel(ASP.NET_Core_web_apps_with_EF_Core.Data.ContosoPizzaContext context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }
    }
}
