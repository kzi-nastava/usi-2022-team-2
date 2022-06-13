using HealthCare_System.Core.Anamneses.Model;

namespace HealthCare_System.Core.Anamneses.Repository
{
    public interface IAnamnesisRepo
    {
        string Path { get; set; }

        Anamnesis FindById(int id);
        int GenerateId();
        void Serialize();
        void Update(int id, string description);
    }
}