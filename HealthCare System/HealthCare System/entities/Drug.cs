using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    enum IngredientStatus
    { 
        OnHold,
        Accepted,
        Rejected
    }

    class Drug
    {
        int id;
        string name;
        List<Ingredient> ingredients;
        IngredientStatus status;

        public Drug() { }

        public Drug(int id, string name, List<Ingredient> ingredients, IngredientStatus status)
        {
            this.id = id;
            this.name = name;
            this.ingredients = ingredients;
            this.status = status;
        }

        public Drug(int id, string name, IngredientStatus status)
        {
            this.id = id;
            this.name = name;
            this.ingredients = null;
            this.status = status;
        }

        public Drug(Drug drug) 
        {
            this.id = drug.id;
            this.name = drug.name;
            this.ingredients = drug.ingredients;
            this.status = drug.status;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        internal List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        [JsonPropertyName("status")]
        internal IngredientStatus Status { get => status; set => status = value; }

        public override string ToString()
        {
            return "Drug[" + "name: " + this.name + " status: "+ this.status +"]";
        }
    }
}
