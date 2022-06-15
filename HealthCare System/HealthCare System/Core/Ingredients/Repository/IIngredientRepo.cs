using HealthCare_System.Core.Ingredients.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Ingredients.Repository
{
    public interface IIngredientRepo
    {
        string Path { get; set; }

        List<Ingredient> Ingredients { get; set; }

        void Add(Ingredient ingredient);

        Ingredient Add(string name);

        void Delete(Ingredient ingredient);

        Ingredient FindById(int id);

        int GenerateId();

        void Serialize();
    }
}