using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    enum TypeOfRoom 
    {
        OperationHall,
        ExaminationHall,
        Storage,
        RelaxRoom,
        Other
    }
    class Room
    {
        int id;
        string name;
        TypeOfRoom type;
        Dictionary<int, int> equipmentAmount;

        
        public Room(){}

        public Room(int id, string name, TypeOfRoom type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.equipmentAmount = null;
        }

        public Room(int id, string name, TypeOfRoom type, Dictionary<int, int> equipmentAmount)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.equipmentAmount = equipmentAmount;
        }

        public Room(Room room)
        {
            this.id = room.id;
            this.name = room.name;
            this.type = room.type;
            this.equipmentAmount = room.equipmentAmount;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonPropertyName("type")]
        internal TypeOfRoom Type { get => type; set => type = value; }

        [JsonIgnore]
        public Dictionary<int, int> EquipmentAmount { get => equipmentAmount; set => equipmentAmount = value; }

        public override string ToString()
        {
            return "Drug[" + "name: " + this.name + " type: " + this.type + "]";
        }
    }
}
