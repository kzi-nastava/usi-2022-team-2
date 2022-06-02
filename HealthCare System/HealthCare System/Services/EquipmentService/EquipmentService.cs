using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.EquipmentRepo;
using HealthCare_System.Model;
using System.Reflection;
using HealthCare_System.Services.RoomService;

namespace HealthCare_System.Services.EquipmentService
{
    class EquipmentService
    {
        EquipmentRepo equipmentRepo;
        RoomService.RoomService roomService; //?

        public EquipmentService()
        {
            EquipmentRepoFactory equipmentRepoFactory = new();
            equipmentRepo = equipmentRepoFactory.CreateEquipmentRepository();
        }

        public List<Equipment> Equipment()
        {
            return equipmentRepo.Equipment;
        }

        public EquipmentRepo EquipmentRepo { get => equipmentRepo; }

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

        public void RoomTypeFilter(string roomType, Dictionary<Equipment, int> equipmentAmount)
        {
            roomService = new();
            foreach (Room room in roomService.Rooms())
            {
                if (roomType != room.Type.ToString())
                {
                    foreach (KeyValuePair<Equipment, int> equipmentAmountEntry in equipmentAmount)
                    {
                        equipmentAmount[equipmentAmountEntry.Key] -= room.EquipmentAmount[equipmentAmountEntry.Key];
                    }
                }
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

        public Dictionary<Equipment, int> GetEquipmentFromAllRooms()
        {
            roomService = new();
            Dictionary<Equipment, int> equipmentAmountAllRooms = new Dictionary<Equipment, int>();
            foreach (Room room in roomService.Rooms())
            {
                foreach (KeyValuePair<Equipment, int> equipmentAmountRoom in room.EquipmentAmount)
                {
                    if (equipmentAmountAllRooms.ContainsKey(equipmentAmountRoom.Key))
                    {
                        equipmentAmountAllRooms[equipmentAmountRoom.Key] += equipmentAmountRoom.Value;
                    }
                    else
                    {
                        equipmentAmountAllRooms[equipmentAmountRoom.Key] = equipmentAmountRoom.Value;
                    }
                }
            }
            return equipmentAmountAllRooms;
        }

        public void ApplyEquipmentFilters(string roomType, string amount, string equipmentType,
            Dictionary<Equipment, int> equipmentAmount)
        {
            if (roomType != "All")
            {
                RoomTypeFilter(roomType, equipmentAmount);
            }

            if (amount != "All")
            {
                AmountFilter(amount, equipmentAmount);
            }

            if (equipmentType != "All")
            {
                EquipmentTypeFilter(equipmentType, equipmentAmount);
            }
        }

        public Dictionary<Equipment, int> InitalizeEquipment()
        {
            Dictionary<Equipment, int> equipmentAmount = new Dictionary<Equipment, int>();
            foreach (Equipment equipment in equipmentRepo.Equipment)
            {
                equipmentAmount[equipment] = 0;
            }
            return equipmentAmount;
        }
    }
}
