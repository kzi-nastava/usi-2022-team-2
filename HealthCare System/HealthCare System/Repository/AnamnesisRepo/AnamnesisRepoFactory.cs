using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.AnamnesisRepo
{
    class AnamnesisRepoFactory
    {
        private AnamnesisRepo anamnesisRepo;

        public AnamnesisRepo CreateAnamnesisRepository()
        {
            if (anamnesisRepo == null)
                anamnesisRepo = new AnamnesisRepo();

            return anamnesisRepo;
        }
    }
}
