namespace WebProject.Models
{
    public class ServiciuData
    {
        public IEnumerable<Serviciu> Servicii { get; set; }
        public IEnumerable<Categorie> Categorii { get; set; }
        public IEnumerable<ServiciuCategorie> ServiciuCategorii { get; set; }
    }
}
