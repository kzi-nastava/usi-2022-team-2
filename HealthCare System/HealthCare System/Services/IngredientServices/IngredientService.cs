using System;
using System.Collections.Generic;
using HealthCare_System.Repository.IngredientRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.DrugServices;
using HealthCare_System.Model.Dto;

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

        public void Create(IngredientDTO ingredientDTO)
        {
            if (ingredientDTO.Name.Length > 30 || ingredientDTO.Name.Length < 5)
                throw new Exception();
            Ingredient ingredient = new Ingredient(ingredientDTO);
            ingredientRepo.Add(ingredient);
        }

        public void Update(IngredientDTO ingredientDTO, Ingredient ingredient)
        {

            if (ingredientDTO.Name.Length > 30 || ingredientDTO.Name.Length < 5)
                throw new Exception();
            ingredient.Name = ingredientDTO.Name;
            ingredientRepo.Serialize();
        }

        public void Delete(Ingredient ingredient)
        {
            ingredientRepo.Delete(ingredient);
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
