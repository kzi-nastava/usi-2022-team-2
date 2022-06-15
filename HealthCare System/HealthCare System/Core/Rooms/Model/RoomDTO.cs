using HealthCare_System.Core.Equipments.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Rooms.Model
{
    public class RoomDto
    {
        int id;
        string name;
        TypeOfRoom type;
        Dictionary<Equipment, int> equipmentAmount;

        public RoomDto(int id, string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.equipmentAmount = equipmentAmount;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public TypeOfRoom Type { get => type; set => type = value; }
        public Dictionary<Equipment, int> EquipmentAmount { get => equipmentAmount; set => equipmentAmount = value; }
    }
}
