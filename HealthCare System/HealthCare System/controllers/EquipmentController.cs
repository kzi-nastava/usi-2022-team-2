﻿using HealthCare_System.Model;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class EquipmentController
    {
        List<Equipment> equipment;
        string path;

        public EquipmentController()
        {
            path = "../../../data/entities/Equipment.json";
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

        public void AmountFilter(string amount, Dictionary<Equipment, int> equipmentAmount)
        {
            if (amount == "0-10")
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount) 
                {
                    if (equipmentAmount[equipmentAmountEntry.Key] >= 10 || equipmentAmount[equipmentAmountEntry.Key] <= 0)
                        equipmentAmount.Remove(equipmentAmountEntry.Key);
                }
            }
            else if (amount == "10+")
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
                {
                    if (equipmentAmount[equipmentAmountEntry.Key] < 10)
                        equipmentAmount.Remove(equipmentAmountEntry.Key);
                }
            }
            else
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
                {
                    if (equipmentAmount[equipmentAmountEntry.Key] != 0)
                        equipmentAmount.Remove(equipmentAmountEntry.Key);
                }
            }
        }
        public void EquipmentTypeFilter(string equipmentType, Dictionary<Equipment, int> equipmentAmount) 
        {
            foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
            {
                if (equipmentAmountEntry.Key.Type.ToString() != equipmentType)
                    equipmentAmount.Remove(equipmentAmountEntry.Key);
            }
        }

        public void EquipmentQuery(string value, Dictionary<Equipment, int> equipmentAmount)
        {
            foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
            {
                bool containsValue = false;
                foreach (PropertyInfo prop in equipmentAmountEntry.Key.GetType().GetProperties()) 
                {
                    try
                    {
                        string checkProp = prop.GetValue(equipmentAmountEntry.Key).ToString().ToLower();
                        if (checkProp.Contains(value.ToLower())) 
                        {
                            containsValue = true;
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
                if (!containsValue)   
                    equipmentAmount.Remove(equipmentAmountEntry.Key);
            }
        }
    }
}
