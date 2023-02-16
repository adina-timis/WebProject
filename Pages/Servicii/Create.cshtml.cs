using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Servicii
{
    public class CreateModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public CreateModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MarcaID"] = new SelectList(_context.Set<Marca>(), "ID", "MarcaNume");
            ViewData["PersonalID"] = new SelectList(_context.Set<Personal>(), "ID", "Nume");
            ViewData["PersonalID"] = new SelectList(_context.Set<Personal>(), "ID", "Prenume");
            return Page();
        }

        [BindProperty]
        public Serviciu Serviciu { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Serviciu.Add(Serviciu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
