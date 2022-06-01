using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.RenovationRepo
{
    class SplittingRenovationRepoFactory
    {
        private SplittingRenovationRepo splittingRenovationRepo;

        public SplittingRenovationRepo CreateSplittingRenovationRepository()
        {
            if (splittingRenovationRepo == null)
                splittingRenovationRepo = new SplittingRenovationRepo();

            return splittingRenovationRepo;
        }
    }
}
