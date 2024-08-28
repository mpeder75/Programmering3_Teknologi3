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
    public class IndexModel : PageModel
    {
        // variabel af datatypen ContosoPizzaContext 
        private readonly ContosoPizzaContext _context;

        // variabel ContosoPizzaContext injectes i constructor og initializeres
        // nu kan vi hente date fra db gennem ContosoPizzaContext
        public IndexModel(ContosoPizzaContext context)
        {
            _context = context;
        }

        // Property collection tilgåes fra denne property
        public IList<Product> Product { get;set; } = default!;

        // Data fra Product hentes og gemmes i Lise ovenover
        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
