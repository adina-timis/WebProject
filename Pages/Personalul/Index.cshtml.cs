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
    public class IndexModel : PageModel
    {
        private readonly WebProject.Data.WebProjectContext _context;

        public IndexModel(WebProject.Data.WebProjectContext context)
        {
            _context = context;
        }

        public IList<Personal> Personal { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Personal != null)
            {
                Personal = await _context.Personal.ToListAsync();
            }
        }
    }
}
