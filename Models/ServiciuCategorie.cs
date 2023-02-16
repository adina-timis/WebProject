namespace WebProject.Models
{
    public class ServiciuCategorie
    {
        public int ID { get; set; }
        public int ServiciuID { get; set; }
        public Serviciu Serviciu { get; set; }
        public int CategorieID { get; set; }
        public Categorie Categorie { get; set; }
    }
}
