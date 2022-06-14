using HealthCare_System.Core.Equipments;
using HealthCare_System.Core.Equipments.Model;
using System.Collections.Generic;

namespace HealthCare_System.gui.Controller
{
    class EquipmentController
    {
        private readonly IEquipmentService equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        public void AmountFilter(string amount, Dictionary<Equipment, int> equipmentAmount)
        {
            equipmentService.AmountFilter(amount, equipmentAmount);
        }

        public void ApplyEquipmentFilters(string roomType, string amount, string equipmentType, Dictionary<Equipment, int> equipmentAmount)
        {
            equipmentService.ApplyEquipmentFilters(roomType, amount, equipmentType, equipmentAmount);
        }

        public List<Equipment> Equipment()
        {
            return equipmentService.Equipment();
        }

        public void EquipmentQuery(string value, Dictionary<Equipment, int> equipmentAmount)
        {
            equipmentService.EquipmentQuery(value, equipmentAmount);
        }

        public void EquipmentTypeFilter(string equipmentType, Dictionary<Equipment, int> equipmentAmount)
        {
            equipmentService.EquipmentTypeFilter(equipmentType, equipmentAmount);
        }

        public Dictionary<Equipment, int> GetEquipmentFromAllRooms()
        {
            return equipmentService.GetEquipmentFromAllRooms();
        }

        public Dictionary<Equipment, int> InitalizeEquipment()
        {
            return equipmentService.InitalizeEquipment();
        }

        public void RoomTypeFilter(string roomType, Dictionary<Equipment, int> equipmentAmount)
        {
            equipmentService.RoomTypeFilter(roomType, equipmentAmount);
        }

        public Equipment FindById(int id)
        {
            return equipmentService .FindById(id);
        }

        public void Serialize()
        {
            equipmentService.Serialize();
        }
    }
}
