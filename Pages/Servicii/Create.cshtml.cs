using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Servicii
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : ServiciuCategoriiPageModel
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
            var serviciu = new Serviciu();
            serviciu.ServiciuCategorii = new List<ServiciuCategorie>();
            PopulateAssignedCategoryData(_context, serviciu);
            var personalList = _context.Personal.Select(x => new
            {
                x.ID,
                FullName = x.Nume + " " + x.Prenume
            });
            ViewData["PersonalID"] = new SelectList(personalList, "ID", "FullName");
            ViewData["MarcaID"] = new SelectList(_context.Marca, "ID", "MarcaNume");
            return Page();
            return Page();
        }

        [BindProperty]
        public Serviciu Serviciu { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newServiciu = Serviciu;
            if (selectedCategories != null)
            {
                newServiciu.ServiciuCategorii = new List<ServiciuCategorie>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new ServiciuCategorie
                    {
                        CategorieID = int.Parse(cat)
                    };
                    newServiciu.ServiciuCategorii.Add(catToAdd);
                }
            }
            //if (await TryUpdateModelAsync<Book>(
            //newBook,
            //"Book",
            //i => i.Title, i => i.Author,
            //i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            //}
            _context.Serviciu.Add(newServiciu);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            PopulateAssignedCategoryData(_context, newServiciu);
            return Page();
        }
    }
}