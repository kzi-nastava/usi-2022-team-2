using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserService
{
    class PatientService
    {
        PatientRepo patientRepo;

        public PatientService()
        {
            PatientRepoFactory patientRepoFactory = new();
            patientRepo = patientRepoFactory.CreatePatientRepository();
        }

        public PatientRepo PatientRepo { get => patientRepo;}
    }
}
