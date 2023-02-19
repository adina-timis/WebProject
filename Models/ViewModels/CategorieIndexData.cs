namespace WebProject.Models.ViewModels
{
    public class CategorieIndexData
    {
        public IEnumerable<Categorie> Categorii { get; set; }
        public IEnumerable<ServiciuCategorie> ServiciuCategorii { get; set; }
        public IEnumerable<Serviciu> Servicii { get; set; }
    }
}
