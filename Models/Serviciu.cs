using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Xml.Linq;


namespace WebProject.Models
{
    public class Serviciu
    {
        public int ID { get; set; }

        [Display(Name = "Nume Serviciu")]
        [StringLength(150, MinimumLength = 3)]
        [Required]
        public string Nume { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Pret { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DataInfiintarii { get; set; }

        public int? PersonalID { get; set; }
        public Personal? Personal { get; set; }  //navigation property
        public int? MarcaID { get; set; }
        public Marca? Marca { get; set; }  //navigation property
    }
}
