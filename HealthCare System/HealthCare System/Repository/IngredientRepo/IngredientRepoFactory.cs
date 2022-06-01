using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.IngredientRepo
{
    class IngredientRepoFactory
    {
        private IngredientRepo ingredientRepo;

        public IngredientRepo CreateIngredientRepository()
        {
            if (ingredientRepo == null)
                ingredientRepo = new IngredientRepo();

            return ingredientRepo;
        }
    }
}
