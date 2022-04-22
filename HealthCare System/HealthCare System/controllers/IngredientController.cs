using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class IngredientController
    {
        List<Ingredient> ingredients;

        public IngredientController()
        {
            this.LoadIngredients();
        }

        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        void LoadIngredients()
        {
            this.ingredients = JsonSerializer.Deserialize<List<Ingredient>>(File.ReadAllText("data/entities/Ingredients.json"));
        }

        public Ingredient FindById(int id)
        {
            foreach (Ingredient ingredient in this.ingredients)
                if (ingredient.Id == id)
                    return ingredient;
            return null;
        }
    }
}
