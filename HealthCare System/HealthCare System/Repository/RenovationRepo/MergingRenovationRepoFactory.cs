using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.RenovationRepo
{
    class MergingRenovationRepoFactory
    {
        private MergingRenovationRepo mergingRenovationRepo;

        public MergingRenovationRepo CreateMergingRenovationRepository()
        {
            if (mergingRenovationRepo == null)
                mergingRenovationRepo = new MergingRenovationRepo();

            return mergingRenovationRepo;
        }
    }
}
