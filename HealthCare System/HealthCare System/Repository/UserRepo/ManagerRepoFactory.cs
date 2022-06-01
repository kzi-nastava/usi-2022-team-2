using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.UserRepo
{
    class ManagerRepoFactory
    {
        private ManagerRepo managerRepo;

        public ManagerRepo CreateManagerRepository()
        {
            if (managerRepo == null)
                managerRepo = new ManagerRepo();

            return managerRepo;
        }
    }
}
