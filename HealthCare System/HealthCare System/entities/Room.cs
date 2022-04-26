using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    enum TypeOfRoom 
    {
        OPERATION_HALL,
        EXAMINATION_HALL,
        STORAGE,
        RELAX_ROOM,
        OTHER
    }
    class Room
    {
        int id;
        string name;
        TypeOfRoom type;
        Dictionary<Equipment, int> equipmentAmount;

        public Room()
        {
            equipmentAmount = new Dictionary<Equipment, int>();
        }

        public Room(int id, string name, TypeOfRoom type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            equipmentAmount = new Dictionary<Equipment, int>();
        }

        public Room(int id, string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount)
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
        public Dictionary<Equipment, int> EquipmentAmount { get => equipmentAmount; set => equipmentAmount = value; }

        public override string ToString()
        {
            return "Room[id: " + this.id + ", name: " + this.name + " type: " + this.type + "]";
        }
    }
}
