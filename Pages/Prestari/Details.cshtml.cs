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
    public class DetailsModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public DetailsModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public Prestare Prestare { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prestare == null)
            {
                return NotFound();
            }

            var prestare = await _context.Prestare.Include(b => b.Client).Include(b => b.Serviciu).FirstOrDefaultAsync(m => m.ID == id);
            if (prestare == null)
            {
                return NotFound();
            }
            else
            {
                Prestare = prestare;
            }
            return Page();
        }
    }
}