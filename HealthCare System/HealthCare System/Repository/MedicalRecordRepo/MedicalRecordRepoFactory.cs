using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.MedicalRecordRepo
{
    class MedicalRecordRepoFactory
    {
        private MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordRepo CreateMedicalRecordRepository()
        {
            if (medicalRecordRepo == null)
                medicalRecordRepo = new MedicalRecordRepo();

            return medicalRecordRepo;
        }
    }
}
