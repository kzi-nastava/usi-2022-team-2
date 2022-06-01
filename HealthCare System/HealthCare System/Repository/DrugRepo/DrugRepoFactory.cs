using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.DrugRepo
{
    class DrugRepoFactory
    {
        private DrugRepo drugRepo;

        public DrugRepo CreateDrugRepository()
        {
            if (drugRepo == null)
                drugRepo = new DrugRepo();

            return drugRepo;
        }
    }
}
