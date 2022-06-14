using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.Ingredients.Repository;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Ingredients
{
    public class IngredientService : IIngredientService
    {
        IIngredientRepo ingredientRepo;
        IDrugService drugService;

        public IngredientService(IIngredientRepo ingredientRepo, IDrugService drugService)
        {
            this.ingredientRepo = ingredientRepo;
            this.drugService = drugService;
        }

        public IIngredientRepo IngredientRepo { get => ingredientRepo; }

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
            Serialize();
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

        public Ingredient FindById(int id)
        {
            return ingredientRepo.FindById(id);
        }

        public int GenerateId()
        {
            return ingredientRepo.GenerateId();
        }

        public void Serialize()
        {
            ingredientRepo.Serialize();
        }
    }
}
