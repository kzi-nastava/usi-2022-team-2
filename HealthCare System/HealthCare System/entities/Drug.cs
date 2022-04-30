using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public enum DrugStatus
    { 
        ON_HOLD,
        ACCEPTED,
        REJECTED
    }

    public class Drug
    {
        int id;
        string name;
        List<Ingredient> ingredients;
        DrugStatus status;

        public Drug()
        {
            this.ingredients = new List<Ingredient>();
        }

        public Drug(int id, string name, List<Ingredient> ingredients, DrugStatus status)
        {
            this.id = id;
            this.name = name;
            this.ingredients = ingredients;
            this.status = status;
        }

        public Drug(int id, string name, DrugStatus status)
        {
            this.id = id;
            this.name = name;
            this.ingredients = null;
            this.status = status;
            ingredients = new List<Ingredient>();
        }

        public Drug(Drug drug) 
        {
            id = drug.id;
            name = drug.name;
            ingredients = drug.ingredients;
            status = drug.status;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        [JsonPropertyName("status")]
        public DrugStatus Status { get => status; set => status = value; }

        public override string ToString()
        {
            return "Drug[" + "name: " + name + " status: "+ status +"]";
        }
    }
}
