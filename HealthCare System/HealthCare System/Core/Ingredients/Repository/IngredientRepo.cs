﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.Ingredients.Model;

namespace HealthCare_System.Core.Ingredients.Repository
{
    public class IngredientRepo : IIngredientRepo
    {
        List<Ingredient> ingredients;
        string path;

        public IngredientRepo()
        {
            path = "../../../data/entities/Ingredients.json";
            Load();
        }

        public IngredientRepo(string path)
        {
            this.path = path;
            Load();
        }

        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

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

        public Ingredient Add(string name)
        {
            Ingredient ingredient = new Ingredient(GenerateId(), name);
            ingredients.Add(ingredient);
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

        public void Add(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
            Serialize();
        }

        public void Delete(Ingredient ingredient)
        {
            ingredients.Remove(ingredient);
            Serialize();
        }
    }
}
