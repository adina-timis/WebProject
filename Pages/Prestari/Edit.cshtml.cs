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
    public class EditModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public EditModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prestare Prestare { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prestare == null)
            {
                return NotFound();
            }

            var prestare = await _context.Prestare.FirstOrDefaultAsync(m => m.ID == id);
            if (prestare == null)
            {
                return NotFound();
            }
            Prestare = prestare;
            var serviciuList = _context.Serviciu
                .Include(b => b.Personal)
                .Select(x => new
                {
                    x.ID,
                    ServiciuFullName = x.Nume + " - " + x.Personal.Nume + " " + x.Personal.Prenume
                });
            ViewData["ServiciuID"] = new SelectList(serviciuList, "ID", "ServiciuFullName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Prestare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestareExists(Prestare.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PrestareExists(int id)
        {
            return _context.Prestare.Any(e => e.ID == id);
        }
    }
}
