using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class EquipmentController
    {
        List<Equipment> equipment;
        string path;

        public EquipmentController()
        {
            path = "data/entities/Equipment.json";
            Load();
        }

        public EquipmentController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Equipment> Equipment { get => equipment; set => equipment = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            equipment = JsonSerializer.Deserialize<List<Equipment>>(File.ReadAllText(path));
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment equipment in equipment)
                if (equipment.Id == id)
                    return equipment;
            return null;
        }

        public void Serialize()
        {
            string equipmentJson = JsonSerializer.Serialize(equipment, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, equipmentJson);
        }
    }
}
