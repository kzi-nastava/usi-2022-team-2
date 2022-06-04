using System;
using System.Collections.Generic;
using HealthCare_System.Repository.IngredientRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.DrugServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.Services.IngredientServices
{
    public class IngredientService
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

        public void Create(IngredientDto ingredientDto)
        {
            if (ingredientDto.Name.Length > 30 || ingredientDto.Name.Length < 5)
                throw new Exception();
            Ingredient ingredient = new Ingredient(ingredientDto);
            ingredientRepo.Add(ingredient);
        }

        public void Update(IngredientDto ingredientDto, Ingredient ingredient)
        {

            if (ingredientDto.Name.Length > 30 || ingredientDto.Name.Length < 5)
                throw new Exception();
            ingredient.Name = ingredientDto.Name;
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
