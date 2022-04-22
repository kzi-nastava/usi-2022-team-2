using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class IngredientController
    {
        List<Ingredient> ingredients;

        public IngredientController()
        {
            Load();
        }

        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        void Load()
        {
            ingredients = JsonSerializer.Deserialize<List<Ingredient>>(File.ReadAllText("data/entities/Ingredients.json"));
        }

        public Ingredient FindById(int id)
        {
            foreach (Ingredient ingredient in ingredients)
                if (ingredient.Id == id)
                    return ingredient;
            return null;
        }
    }
}
