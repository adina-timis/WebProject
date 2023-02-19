using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Prestari
{
    public class IndexModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public IndexModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IList<Prestare> Prestare { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Prestare != null)
            {
                Prestare = await _context.Prestare
                .Include(b => b.Serviciu)
                    .ThenInclude(b => b.Personal)
                .Include(b => b.Client).ToListAsync();
            }
        }
    }
}
