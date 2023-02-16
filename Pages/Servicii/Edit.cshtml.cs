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

namespace WebProject.Pages.Servicii
{
    public class EditModel : ServiciuCategoriiPageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public EditModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Serviciu Serviciu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serviciu == null)
            {
                return NotFound();
            }

            var serviciu =  await _context.Serviciu.FirstOrDefaultAsync(m => m.ID == id);
            Serviciu = await _context.Serviciu
                .Include(b => b.Marca)
                .Include(b => b.ServiciuCategorii).ThenInclude(b => b.Categorie)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (serviciu == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Serviciu);

            var authorList = _context.Personal.Select(x => new
            {
                x.ID,
                FullName = x.Nume + " " + x.Prenume
            });

            Serviciu = serviciu;
            ViewData["MarcaID"] = new SelectList(_context.Set<Marca>(), "ID", "MarcaNume");
            ViewData["PersonalID"] = new SelectList(_context.Set<Personal>(), "ID", "PersonalNume");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviciuToUpdate = await _context.Serviciu
                .Include(i => i.Marca)
                .Include(i => i.ServiciuCategorii)
                    .ThenInclude(i => i.Categorie)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (serviciuToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Serviciu>(
                serviciuToUpdate,
                "Serviciu",
                i => i.Nume, i => i.Personal,
                 i => i.Pret, i => i.DataInfiintarii, i => i.Marca))
            {
                UpdateServiciuCategorii(_context, selectedCategories, serviciuToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care 
            //este editata 
            UpdateServiciuCategorii(_context, selectedCategories, serviciuToUpdate);
            PopulateAssignedCategoryData(_context, serviciuToUpdate);
            return Page();
        }
    }
}
