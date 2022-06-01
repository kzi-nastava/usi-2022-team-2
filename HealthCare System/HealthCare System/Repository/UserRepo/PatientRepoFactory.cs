using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.UserRepo
{
    class PatientRepoFactory
    {
        private PatientRepo patientRepo;

        public PatientRepo CreatePatientRepository()
        {
            if (patientRepo == null)
                patientRepo = new PatientRepo();

            return patientRepo;
        }
    }
}
