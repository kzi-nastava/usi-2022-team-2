using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.Ingredients.Model;
using System.Collections.Generic;

namespace HealthCare_System.gui.Controller
{
    class IngredientController
    {
        private readonly IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public void Create(IngredientDto ingredientDto)
        {
            ingredientService.Create(ingredientDto);
        }

        public void Delete(Ingredient ingredient)
        {
            ingredientService.Delete(ingredient);
        }

        public List<Ingredient> Ingredients()
        {
            return ingredientService.Ingredients();
        }

        public bool IsIngredientAvailableForChange(Ingredient ingredient)
        {
            return ingredientService.IsIngredientAvailableForChange(ingredient);
        }

        public void Update(IngredientDto ingredientDto, Ingredient ingredient)
        {
            ingredientService.Update(ingredientDto, ingredient);
        }

        public Ingredient FindById(int id)
        {
            return ingredientService.FindById(id);
        }


        public int GenerateId()
        {
            return ingredientService.GenerateId();
        }

        public void Serialize()
        {
            ingredientService.Serialize();
        }

    }
}
