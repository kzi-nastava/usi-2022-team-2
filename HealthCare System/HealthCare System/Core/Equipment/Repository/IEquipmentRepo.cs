using HealthCare_System.Core.Equipments.Model;

namespace HealthCare_System.Core.Equipments.Repository
{
    public interface IEquipmentRepo
    {
        string Path { get; set; }

        Equipment FindById(int id);

        void Serialize();
    }
}