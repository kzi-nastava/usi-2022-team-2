using HealthCare_System.Core.Equipments.Model;
using HealthCare_System.Core.Equipments.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Equipments
{
    public interface IEquipmentService
    {
        IEquipmentRepo EquipmentRepo { get; }

        void AmountFilter(string amount, Dictionary<Equipment, int> equipmentAmount);

        void ApplyEquipmentFilters(string roomType, string amount, string equipmentType, Dictionary<Equipment, int> equipmentAmount);

        List<Equipment> Equipment();

        void EquipmentQuery(string value, Dictionary<Equipment, int> equipmentAmount);

        void EquipmentTypeFilter(string equipmentType, Dictionary<Equipment, int> equipmentAmount);

        Dictionary<Equipment, int> GetEquipmentFromAllRooms();

        Dictionary<Equipment, int> InitalizeEquipment();

        void RoomTypeFilter(string roomType, Dictionary<Equipment, int> equipmentAmount);
    }
}