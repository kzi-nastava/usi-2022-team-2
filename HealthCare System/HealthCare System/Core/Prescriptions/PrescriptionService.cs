using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCare_System.Repository.PrescriptionRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.MedicalRecordServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.Core.Prescriptions
{
    public class PrescriptionService
    {
        PrescriptionRepo prescriptionRepo;
        MedicalRecordService medicalRecordService;

        public PrescriptionService(PrescriptionRepo prescriptionRepo, MedicalRecordService medicalRecordService)
        {
            this.prescriptionRepo = prescriptionRepo;
            this.medicalRecordService = medicalRecordService;
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

        public void AddPrescrition(PrescriptionDto prescriptionDto)
        {
            Prescription prescription = new(prescriptionDto);
            MedicalRecord medicalRecord = medicalRecordService.FindById(prescription.MedicalRecord.Id);
            medicalRecord.ValidatePrescription(prescription);
            medicalRecord.Prescriptions.Add(prescription);

            prescriptionRepo.Add(prescription);
        }
    }
}
