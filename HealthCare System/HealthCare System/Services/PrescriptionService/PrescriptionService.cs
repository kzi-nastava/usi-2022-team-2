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

        private void DeletePrescriptions(MedicalRecord medicalRecord)
        {
            for (int i = prescriptionController.Prescriptions.Count - 1; i >= 0; i--)
            {
                if (prescriptionController.Prescriptions[i].MedicalRecord == medicalRecord)
                {
                    prescriptionController.Prescriptions.RemoveAt(i);
                }
            }
            prescriptionController.Serialize();
        }

        public void AddPrescrition(Prescription prescription)
        {
            MedicalRecord medicalRecord = medicalRecordController.FindById(prescription.MedicalRecord.Id);
            medicalRecord.ValidatePrescription(prescription);
            medicalRecord.Prescriptions.Add(prescription);

            prescriptionController.Prescriptions.Add(prescription);
            prescriptionController.Serialize();
        }
    }
}
