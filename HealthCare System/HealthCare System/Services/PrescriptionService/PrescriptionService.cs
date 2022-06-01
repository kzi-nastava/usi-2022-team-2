using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.PrescriptionRepo;

namespace HealthCare_System.Services.PrescriptionService
{
    class PrescriptionService
    {
        PrescriptionRepo prescriptionRepo;

        public PrescriptionService()
        {
            PrescriptionRepoFactory prescriptionRepoFactory = new();
            prescriptionRepo = prescriptionRepoFactory.CreatePrescriptionRepository();
        }

        public PrescriptionRepo PrescriptionRepo { get => prescriptionRepo;}
    }
}
