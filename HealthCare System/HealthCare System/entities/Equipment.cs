using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    enum TypeOfEquipment
    { 
        Examination,
        Operation,
        Furniture,
        Hallway
    }

    

    class Equipment
    {
        int id;
        string name;
        TypeOfEquipment type;
        bool dynamic;

        public Equipment(){ }

        public Equipment(int id, string name, TypeOfEquipment type, bool dynamic)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.dynamic = dynamic;
        }

        public Equipment(Equipment equipment) 
        {
            this.id = equipment.id;
            this.name = equipment.name;
            this.type = equipment.type;
            this.dynamic = equipment.dynamic;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonPropertyName("dynamic")]
        public bool Dynamic { get => dynamic; set => dynamic = value; }

        [JsonPropertyName("type")]
        internal TypeOfEquipment Type { get => type; set => type = value; }

        public override string ToString()
        {
            return "Name: " + this.name;
        }
    }
}
