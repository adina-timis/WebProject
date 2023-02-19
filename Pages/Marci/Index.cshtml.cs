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

namespace WebProject.Pages.Marci
{
    public class IndexModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public IndexModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IList<Marca> Marca { get;set; } = default!;

        public MarcaIndexData MarcaData { get; set; }
        public int MarcaID { get; set; }
        public int ServiciuID { get; set; }
        public async Task OnGetAsync(int? id, int? serviciuID)
        {
            MarcaData = new MarcaIndexData();
            MarcaData.Marci = await _context.Marca
            .Include(i => i.Servicii)
                .ThenInclude(c => c.Personal)
            .OrderBy(i => i.MarcaNume)
            .ToListAsync();
            if (id != null)
            {
                MarcaID = id.Value;
                Marca marca = MarcaData.Marci
                    .Where(i => i.ID == id.Value).Single();
                MarcaData.Servicii = marca.Servicii;
            }
        }
    }
}
