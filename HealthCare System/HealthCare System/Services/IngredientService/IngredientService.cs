using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Services.IngredientService
{
    class IngredientService
    {
        public void Add(string name)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Ingredient ingredient = new Ingredient(GenerateId(), name);
            IngredientRepo.Add(ingredient);
        }
        public void Update(string name, Ingredient ingredient)
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            ingredient.Name = name;
            IngredientRepo.Serialize();
        }
    }
}
