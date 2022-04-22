using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class EquipmentControllers
    {
        List<Equipment> equipments;

        public EquipmentControllers()
        {
            Load();
        }

        public List<Equipment> Equipment
        {
            get { return equipments; }
            set { equipments = value; }
        }

        void Load()
        {
            equipments = JsonSerializer.Deserialize<List<Equipment>>(File.ReadAllText("data/entities/Equipment.json"));
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment equipment in equipments)
                if (equipment.Id == id)
                    return equipment;
            return null;
        }
    }
}
