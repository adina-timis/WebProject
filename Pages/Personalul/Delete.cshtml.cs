using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Personalul
{
    public class DeleteModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public DeleteModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Personal Personal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Personal == null)
            {
                return NotFound();
            }

            var personal = await _context.Personal.FirstOrDefaultAsync(m => m.ID == id);

            if (personal == null)
            {
                return NotFound();
            }
            else 
            {
                Personal = personal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Personal == null)
            {
                return NotFound();
            }
            var personal = await _context.Personal.FindAsync(id);

            if (personal != null)
            {
                Personal = personal;
                _context.Personal.Remove(Personal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
