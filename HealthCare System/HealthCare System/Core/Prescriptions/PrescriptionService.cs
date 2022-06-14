using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions.Model;
using HealthCare_System.Core.Prescriptions.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Prescriptions
{
    public class PrescriptionService : IPrescriptionService
    {
        IPrescriptionRepo prescriptionRepo;
        IMedicalRecordService medicalRecordService;

        public PrescriptionService(IPrescriptionRepo prescriptionRepo, IMedicalRecordService medicalRecordService)
        {
            this.prescriptionRepo = prescriptionRepo;
            this.medicalRecordService = medicalRecordService;
        }

        public IPrescriptionRepo PrescriptionRepo { get => prescriptionRepo; }

        public List<Prescription> Prescriptions()
        {
            return prescriptionRepo.Prescriptions;
        }

        public void DeletePrescriptions(MedicalRecord medicalRecord)
        {
            for (int i = Prescriptions().Count - 1; i >= 0; i--)
            {
                if (prescriptionRepo.Prescriptions[i].MedicalRecord == medicalRecord)
                {
                    prescriptionRepo.Prescriptions.RemoveAt(i);
                }
            }
            Serialize();
        }

        public void AddPrescrition(PrescriptionDto prescriptionDto)
        {
            Prescription prescription = new(prescriptionDto);
            MedicalRecord medicalRecord = medicalRecordService.FindById(prescription.MedicalRecord.Id);
            medicalRecord.ValidatePrescription(prescription);
            medicalRecord.Prescriptions.Add(prescription);

            prescriptionRepo.Add(prescription);
        }

        public Prescription FindById(int id)
        {
            return prescriptionRepo.FindById(id);
        }

        public int GenerateId()
        {
            return prescriptionRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/PrescriptionLinker.csv")
        {
            prescriptionRepo.Serialize();
        }
    }
}
