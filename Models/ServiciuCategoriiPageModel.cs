using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using WebProject.Data;

namespace WebProject.Models
{
    public class ServiciuCategoriiPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(WebProjectContext context,
        Serviciu serviciu)
        {
            var allCategories = context.Categorie;
            var serviciuCategorii = new HashSet<int>(
            serviciu.ServiciuCategorii.Select(c => c.CategorieID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategorieID = cat.ID,
                    Nume = cat.CategorieNume,
                    Asignat = serviciuCategorii.Contains(cat.ID)
                });
            }
        }
        public void UpdateServiciuCategorii(WebProjectContext context,
        string[] selectedCategories, Serviciu serviciuToUpdate)
        {
            if (selectedCategories == null)
            {
                serviciuToUpdate.ServiciuCategorii = new List<ServiciuCategorie>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var serviciuCategorii = new HashSet<int>
            (serviciuToUpdate.ServiciuCategorii.Select(c => c.Categorie.ID));
            foreach (var cat in context.Categorie)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!serviciuCategorii.Contains(cat.ID))
                    {
                        serviciuToUpdate.ServiciuCategorii.Add(
                        new ServiciuCategorie
                        {
                            ServiciuID = serviciuToUpdate.ID,
                            CategorieID = cat.ID
                        });
                    }
                }
                else
                {
                    if (serviciuCategorii.Contains(cat.ID))
                    {
                        ServiciuCategorie courseToRemove
                        = serviciuToUpdate
                        .ServiciuCategorii
                        .SingleOrDefault(i => i.CategorieID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
