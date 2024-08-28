using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.NET_Core_web_apps_with_EF_Core.Data;
using ASP.NET_Core_web_apps_with_EF_Core.Models;

namespace ASP.NET_Core_web_apps_with_EF_Core.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ASP.NET_Core_web_apps_with_EF_Core.Data.ContosoPizzaContext _context;

        public CreateModel(ASP.NET_Core_web_apps_with_EF_Core.Data.ContosoPizzaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
