using HealthCare_System.Repository.AnamnesisRepo;
using System.Collections.Generic;
using HealthCare_System.Model;

namespace HealthCare_System.Services.AnamnesisService
{
    class AnamnesisService
    {
        AnamnesisRepo anamnesisRepo;

        public AnamnesisService()
        {
            AnamnesisRepoFactory anamnesisRepoFactory = new();
            anamnesisRepo = anamnesisRepoFactory.CreateAnamnesisRepository();
        }

        internal AnamnesisRepo AnamnesisRepo { get => anamnesisRepo; }

        public List<Anamnesis> Anamneses()
        {
            return anamnesisRepo.Anamneses;
        }
    }
}
