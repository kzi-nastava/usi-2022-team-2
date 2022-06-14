using System.Collections.Generic;
using HealthCare_System.Model;
using HealthCare_System.Core.Anamneses.Repository;
using HealthCare_System.Core.Anamneses.Model;

namespace HealthCare_System.Core.Anamneses
{
    public class AnamnesisService : IAnamnesisService
    {
        private readonly IAnamnesisRepo anamnesisRepo;

        public AnamnesisService(IAnamnesisRepo anamnesisRepo)
        {
            this.anamnesisRepo = anamnesisRepo;
        }

        public IAnamnesisRepo AnamnesisRepo { get => anamnesisRepo; }

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
