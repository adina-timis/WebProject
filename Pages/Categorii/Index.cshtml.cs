using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.ViewModels;


namespace WebProject.Pages.Categorii
{
    public class IndexModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public IndexModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IList<Categorie> Categorie { get; set; } = default!;
        public CategorieIndexData CategorieData { get; set; }
        public int CategorieID { get; set; }
        public int ServiciuID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategorieData = new CategorieIndexData();
            CategorieData.Categorii = await _context.Categorie
            .Include(i => i.ServiciuCategorii)
            .ThenInclude(i => i.Serviciu)
            .ThenInclude(c => c.Personal)
            .OrderBy(i => i.CategorieNume)
            .ToListAsync();
            if (id != null)
            {
                CategorieID = id.Value;
                Categorie category = CategorieData.Categorii
                .Where(i => i.ID == id.Value).Single();
                CategorieData.ServiciuCategorii = category.ServiciuCategorii;
            }
        } 
    }  
}
