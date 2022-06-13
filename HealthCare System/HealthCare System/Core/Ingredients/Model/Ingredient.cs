using HealthCare_System.Model.Dto;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Ingredients.Model
{
    public class Ingredient
    {
        int id;
        string name;

        public Ingredient() { }

        public Ingredient(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Ingredient(Ingredient ingredient)
        {
            id = ingredient.id;
            name = ingredient.name;
        }

        public Ingredient(IngredientDto ingredientDto)
        {
            id = ingredientDto.Id;
            name = ingredientDto.Name;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return "Ingredient[" + "name: " + name + "]";
        }
    }
}
