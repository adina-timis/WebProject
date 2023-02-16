using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Data
{
    public class WebProjectContext : DbContext
    {
        public WebProjectContext (DbContextOptions<WebProjectContext> options)
            : base(options)
        {
        }

        public DbSet<WebProject.Models.Serviciu> Serviciu { get; set; } = default!;

        public DbSet<WebProject.Models.Personal> Personal { get; set; }

        public DbSet<WebProject.Models.Marca> Marca { get; set; }

        public DbSet<WebProject.Models.Categorie> Categorie { get; set; }
    }
}
