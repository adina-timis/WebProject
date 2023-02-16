using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebProject.Models
{
    public class Personal
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Nume + " " + Prenume;
            }
        }
        public ICollection<Serviciu>? Servicii { get; set; } //navigation property
    }
}
