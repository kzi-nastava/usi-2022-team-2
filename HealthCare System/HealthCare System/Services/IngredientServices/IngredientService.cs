using System;
using System.Collections.Generic;
using HealthCare_System.Repository.IngredientRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.DrugServices;

namespace HealthCare_System.Services.IngredientServices
{
    class IngredientService
    {
        IngredientRepo ingredientRepo;
        DrugService drugService;

        public IngredientService(IngredientRepo ingredientRepo, DrugService drugService)
        {
            this.ingredientRepo = ingredientRepo;
            this.drugService = drugService;
        }

        public IngredientRepo IngredientRepo { get => ingredientRepo; }

        public List<Ingredient> Ingredients()
        {
            return ingredientRepo.Ingredients;
        }

        public void Add(string name)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Ingredient ingredient = new Ingredient(ingredientRepo.GenerateId(), name);
            ingredientRepo.Add(ingredient);
        }

        public void Update(string name, Ingredient ingredient)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            ingredient.Name = name;
            ingredientRepo.Serialize();
        }

        public bool IsIngredientAvailableForChange(Ingredient ingredient)
        {
            bool available = true;

            foreach (Drug drug in drugService.Drugs())
            {
                if (drug.Ingredients.Contains(ingredient))
                {
                    available = false;
                    break;
                }
            }

            return available;
        }
    }
}
