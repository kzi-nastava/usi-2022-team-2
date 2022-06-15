using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.Ingredients.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Ingredients
{
    public interface IIngredientService
    {
        IIngredientRepo IngredientRepo { get; }

        public IDrugService DrugService { get; set; }

        void Create(IngredientDto ingredientDto);

        void Delete(Ingredient ingredient);

        List<Ingredient> Ingredients();

        bool IsIngredientAvailableForChange(Ingredient ingredient);

        void Update(IngredientDto ingredientDto, Ingredient ingredient);

        Ingredient FindById(int id);

        int GenerateId();

        void Serialize();
    }
}