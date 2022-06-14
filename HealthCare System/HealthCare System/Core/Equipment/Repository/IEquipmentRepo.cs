using HealthCare_System.Core.Equipments.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Equipments.Repository
{
    public interface IEquipmentRepo
    {
        string Path { get; set; }

        List<Equipment> Equipment { get; set; }

        Equipment FindById(int id);

        void Serialize();
    }
}