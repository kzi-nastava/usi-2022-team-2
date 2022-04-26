using System.Text.Json.Serialization;

namespace HealthCare_System.entities
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
            this.id = ingredient.id;
            this.name = ingredient.name;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return "Ingredient[" + "name: " + this.name + "]";
        }
    }
}
