using HealthCare_System.Core.Prescriptions.Model;

namespace HealthCare_System.Core.Prescriptions.Repository
{
    public interface IPrescriptionRepo
    {
        string Path { get; set; }

        void Add(Prescription prescription);
        void Delete(Prescription prescription);
        Prescription FindById(int id);
        int GenerateId();
        void Serialize(string linkPath = "../../../data/links/PrescriptionLinker.csv");
    }
}