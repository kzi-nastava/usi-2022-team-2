using HealthCare_System.Core.Anamneses.Model;
using HealthCare_System.Core.Anamneses.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Anamneses
{
    public interface IAnamnesisService
    {
        List<Anamnesis> Anamneses();

        public Anamnesis FindById(int id);

        public void Serialize();

        public int GenerateId();

        public IAnamnesisRepo AnamnesisRepo { get; }

        void Update(int id, string description);
    }
}