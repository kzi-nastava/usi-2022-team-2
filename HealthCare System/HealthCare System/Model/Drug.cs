using HealthCare_System.Model.Dto;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.Model
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
        string message;

        public Drug()
        {
            this.ingredients = new List<Ingredient>();
        }

        public Drug(int id, string name, List<Ingredient> ingredients, DrugStatus status, string message)
        {
            this.id = id;
            this.name = name;
            this.ingredients = ingredients;
            this.status = status;
            this.message = message;
        }

        public Drug(int id, string name, DrugStatus status, string message)
        {
            this.id = id;
            this.name = name;
            this.ingredients = null;
            this.status = status;
            ingredients = new List<Ingredient>();
            this.message = message;
        }

        public Drug(Drug drug) 
        {
            id = drug.id;
            name = drug.name;
            ingredients = drug.ingredients;
            status = drug.status;
            message = drug.message;
        }

        public Drug(DrugDTO drugDTO)
        {
            id = drugDTO.Id;
            name = drugDTO.Name;
            ingredients = drugDTO.Ingredients;
            status = drugDTO.Status;
            message = drugDTO.Message;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonIgnore]
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        [JsonPropertyName("status")]
        public DrugStatus Status { get => status; set => status = value; }

        [JsonPropertyName("message")]
        public string Message { get => message; set => message = value; }

        public override string ToString()
        {
            return "Drug[" + "name: " + name + " status: "+ status +"]";
        }
    }
}
