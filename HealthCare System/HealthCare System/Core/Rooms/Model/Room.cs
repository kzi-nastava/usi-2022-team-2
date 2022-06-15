using HealthCare_System.Core.Equipments.Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Rooms.Model
{
    public enum TypeOfRoom 
    {
        OPERATION_HALL,
        EXAMINATION_HALL,
        STORAGE,
        RELAX_ROOM,
        OTHER
    }

    public class Room
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
            id = room.id;
            name = room.name;
            type = room.type;
            equipmentAmount = room.equipmentAmount;
        }

        public Room(RoomDto roomDto)
        {
            id = roomDto.Id;
            name = roomDto.Name;
            type = roomDto.Type;
            equipmentAmount = roomDto.EquipmentAmount;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("name")]
        public string Name { get => name; set => name = value; }

        [JsonPropertyName("type")]
        public TypeOfRoom Type { get => type; set => type = value; }

        [JsonIgnore]
        public Dictionary<Equipment, int> EquipmentAmount { get => equipmentAmount; set => equipmentAmount = value; }

        public Dictionary<Equipment, int> FilterDynamicEquipment()
        {
            Dictionary<Equipment, int> dynamicEquipment = new Dictionary<Equipment, int>();

            foreach (KeyValuePair<Equipment, int> entry in equipmentAmount)
                if (entry.Key.Dynamic)
                    dynamicEquipment.Add(entry.Key, entry.Value);

            return dynamicEquipment;
        }

        public override string ToString()
        {
            return "Room[id: " + id + ", name: " + name + " type: " + type + "]";
        }
    }
}
