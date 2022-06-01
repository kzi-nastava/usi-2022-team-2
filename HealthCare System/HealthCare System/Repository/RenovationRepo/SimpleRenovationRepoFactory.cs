using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.RenovationRepo
{
    class SimpleRenovationRepoFactory
    {
        private SimpleRenovationRepo simpleRenovationRepo;

        public SimpleRenovationRepo CreateSimpleRenovationRepository()
        {
            if (simpleRenovationRepo == null)
                simpleRenovationRepo = new SimpleRenovationRepo();

            return simpleRenovationRepo;
        }
    }
}
