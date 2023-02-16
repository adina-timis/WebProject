using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Pages.Servicii
{
    public class IndexModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public IndexModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IList<Serviciu> Serviciu { get;set; } = default!;
        public ServiciuData ServiciuD { get; set; }
        public int ServiciuID { get; set; }
        public int CategorieID { get; set; }
        public string NumeSort { get; set; }
        public string PersonalSort { get; set; }
        public string CurrentFilter { get; set; }
        public async Task OnGetAsync(int? id, int? categorieID, string sortOrder, string searchString)
        {
            ServiciuD = new ServiciuData();

            // using System;
            NumeSort = String.IsNullOrEmpty(sortOrder) ? "nume_desc" : "";
            PersonalSort = String.IsNullOrEmpty(sortOrder) ? "personal_desc" : "";

            ServiciuD.Servicii = await _context.Serviciu
            .Include(b => b.Personal)
            .Include(b => b.Marca)
            .Include(b => b.ServiciuCategorii)
            .ThenInclude(b => b.Categorie)
            .AsNoTracking()
            .OrderBy(b => b.Nume)
            .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ServiciuD.Servicii = ServiciuD.Servicii.Where(s => s.Personal.Nume.Contains(searchString)

               || s.Personal.Prenume.Contains(searchString)
               || s.Nume.Contains(searchString));
            }
            if (id != null)
            {
                ServiciuID = id.Value;
                Serviciu book = ServiciuD.Servicii
                .Where(i => i.ID == id.Value).Single();
                ServiciuD.Categorii = book.ServiciuCategorii.Select(s => s.Categorie);
            }
            switch (sortOrder)
            {
                case "nume_desc":
                    ServiciuD.Servicii = ServiciuD.Servicii.OrderByDescending(s =>
                   s.Nume);
                    break;
                case "personal_desc":
                    ServiciuD.Servicii = ServiciuD.Servicii.OrderByDescending(s =>
                   s.Personal.FullName);
                    break;

            }
        }
    }
}
