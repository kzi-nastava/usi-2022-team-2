using HealthCare_System.Core.Ingredients.Model;

namespace HealthCare_System.Core.Ingredients.Repository
{
    public interface IIngredientRepo
    {
        string Path { get; set; }

        void Add(Ingredient ingredient);
        Ingredient add(string name);
        void Delete(Ingredient ingredient);
        Ingredient FindById(int id);
        int GenerateId();
        void Serialize();
    }
}