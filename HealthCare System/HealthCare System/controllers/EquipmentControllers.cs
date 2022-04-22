using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class EquipmentControllers
    {
        List<Equipment> equipments;

        public EquipmentControllers()
        {
            this.LoadEquipment();
        }

        public List<Equipment> Equipment
        {
            get { return equipments; }
            set { equipments = value; }
        }

        void LoadEquipment()
        {
            this.equipments = JsonSerializer.Deserialize<List<Equipment>>(File.ReadAllText("data/entities/Equipment.json"));
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment equipment in this.equipments)
                if (equipment.Id == id)
                    return equipment;
            return null;
        }
    }
}
