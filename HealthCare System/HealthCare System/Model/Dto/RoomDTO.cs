using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class RoomDTO
    {
        int id;
        string name;
        TypeOfRoom type;
        Dictionary<Equipment, int> equipmentAmount;

        public RoomDTO(int id, string name, TypeOfRoom type, Dictionary<Equipment, int> equipmentAmount)
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
