namespace WebProject.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        public string CategorieNume { get; set; }
        public ICollection<ServiciuCategorie>? ServiciuCategorii { get; set; }
    }
}
