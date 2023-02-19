using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Prestari
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
            var serviciuList = _context.Serviciu
              .Include(b => b.Personal)
              .Select(x => new
              {
                  x.ID,
                  ServiciuFullName = x.Nume + " - " + x.Personal.Nume + " " +
             x.Personal.Prenume
              });
            ViewData["ServiciuID"] = new SelectList(serviciuList, "ID", "ServiciuFullName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Prestare Prestare { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Prestare.Add(Prestare);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
