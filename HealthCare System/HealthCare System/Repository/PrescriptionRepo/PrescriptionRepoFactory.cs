using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.PrescriptionRepo
{
    class PrescriptionRepoFactory
    {
        private PrescriptionRepo prescriptionRepo;

        public PrescriptionRepo CreatePrescriptionRepository()
        {
            if (prescriptionRepo == null)
                prescriptionRepo = new PrescriptionRepo();

            return prescriptionRepo;
        }
    }
}
