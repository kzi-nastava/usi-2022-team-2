using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class IngredientController
    {
        List<Ingredient> ingredients;
        string path;

        public IngredientController()
        {
            path = "../../../data/entities/Ingredients.json";
            Load();
        }

        public IngredientController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            ingredients = JsonSerializer.Deserialize<List<Ingredient>>(File.ReadAllText(path));
        }

        public Ingredient FindById(int id)
        {
            foreach (Ingredient ingredient in ingredients)
                if (ingredient.Id == id)
                    return ingredient;
            return null;
        }

        public Ingredient add(string name)
        {
            Ingredient ingredient = new Ingredient(this.GenerateId(), name);
            this.ingredients.Add(ingredient);
            return ingredient;
        }

        public int GenerateId()
        {
            if (ingredients.Count == 0)
                return 1001;
            return ingredients[^1].Id + 1;
        }

        public void Serialize()
        {
            string ingredientsJson = JsonSerializer.Serialize(ingredients,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, ingredientsJson);
        }

        public void CreateNewIngredient(string name) 
        {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            Ingredient ingredient = new Ingredient(GenerateId(), name);
            ingredients.Add(ingredient);
            Serialize();
        }

        public void UpdateIngredient(string name, Ingredient ingredient) {
            if (name.Length > 30 || name.Length < 5)
                throw new Exception();
            ingredient.Name = name;
            Serialize();
        }

        public void DeleteIngredient(Ingredient ingredient) {
            ingredients.Remove(ingredient);
            Serialize();
        }
    }
}
