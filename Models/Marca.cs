using WebProject.Models;


namespace WebProject.Models
{
    public class Marca
    {
        public int ID { get; set; }
        public string MarcaNume { get; set; }
        public ICollection<Serviciu>? Servicii { get; set; } //navigation property
        
            
        }
}
