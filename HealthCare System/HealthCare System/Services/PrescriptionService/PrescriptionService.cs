using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.PrescriptionRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.MedicalRecordService;

namespace HealthCare_System.Services.PrescriptionService
{
    class PrescriptionService
    {
        PrescriptionRepo prescriptionRepo;
        MedicalRecordService.MedicalRecordService medicalRecordService;

        public PrescriptionService()
        {
            PrescriptionRepoFactory prescriptionRepoFactory = new();
            prescriptionRepo = prescriptionRepoFactory.CreatePrescriptionRepository();
        }

        public PrescriptionRepo PrescriptionRepo { get => prescriptionRepo;}

        public List<Prescription> Prescriptions()
        {
            return prescriptionRepo.Prescriptions;
        }

        public void DeletePrescriptions(MedicalRecord medicalRecord)
        {
            for (int i = prescriptionRepo.Prescriptions.Count - 1; i >= 0; i--)
            {
                if (prescriptionRepo.Prescriptions[i].MedicalRecord == medicalRecord)
                {
                    prescriptionRepo.Prescriptions.RemoveAt(i);
                }
            }
            prescriptionRepo.Serialize();
        }

        public void AddPrescrition(Prescription prescription)
        {
            medicalRecordService = new();
            MedicalRecord medicalRecord = medicalRecordService.FindById(prescription.MedicalRecord.Id);
            medicalRecord.ValidatePrescription(prescription);
            medicalRecord.Prescriptions.Add(prescription);

            prescriptionRepo.Add(prescription);
        }
    }
}
