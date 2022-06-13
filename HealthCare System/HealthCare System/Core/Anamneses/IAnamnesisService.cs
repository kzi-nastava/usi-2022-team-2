using HealthCare_System.Core.Anamneses.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Anamneses
{
    public interface IAnamnesisService
    {
        List<Anamnesis> Anamneses();
        void Update(int id, string description);
    }
}