using HealthCare_System.Repository.AnamnesisRepo;
using System.Collections.Generic;
using HealthCare_System.Model;

namespace HealthCare_System.Services.AnamnesisServices
{
    class AnamnesisService
    {
        AnamnesisRepo anamnesisRepo;

        public AnamnesisService(AnamnesisRepo anamnesisRepo)
        {
            this.anamnesisRepo = anamnesisRepo;
        }

        internal AnamnesisRepo AnamnesisRepo { get => anamnesisRepo; }

        public List<Anamnesis> Anamneses()
        {
            return anamnesisRepo.Anamneses;
        }

        public void Update(int id, string description)
        {
            anamnesisRepo.Update(id, description);
        }
    }
}
