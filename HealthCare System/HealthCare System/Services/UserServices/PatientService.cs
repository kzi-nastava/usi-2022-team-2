using System.Collections.Generic;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Services.PrescriptionServices;
using HealthCare_System.Services.MedicalRecordServices;
using HealthCare_System.Services.IngredientServices;

namespace HealthCare_System.Services.UserServices
{
    class PatientService
    {
        PatientRepo patientRepo;
        SchedulingService schedulingService;
        PrescriptionService prescriptionService;
        MedicalRecordService medicalRecordService;
        IngredientService ingredientService;

        public PatientService(PatientRepo patientRepo, SchedulingService schedulingService, 
            PrescriptionService prescriptionService, MedicalRecordService medicalRecordService, 
            IngredientService ingredientService)
        {
            this.patientRepo = patientRepo;
            this.schedulingService = schedulingService;
            this.prescriptionService = prescriptionService;
            this.medicalRecordService = medicalRecordService;
            this.ingredientService = ingredientService;
        }

        public PatientRepo PatientRepo { get => patientRepo;}

        public List<Patient> Patients()
        {
            return patientRepo.Patients;
        }

        public void DeletePatient(Patient patient)
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;
            try
            {
                schedulingService.DeleteAppointmens(patient);
            }
            catch
            {
                throw;
            }
            prescriptionService.DeletePrescriptions(medicalRecord);

            medicalRecordService.Delete(medicalRecord);
            patientRepo.Delete(patient);

        }
        public void AddPatient(Patient patient, MedicalRecord medRecord)
        {
            patientRepo.Add(patient);

            patient.MedicalRecord = medRecord;
            medRecord.Patient = patient;
            medicalRecordService.MedicalRecordRepo.Serialize();
            ingredientService.IngredientRepo.Serialize();
        }
        public void UpdatePatient()
        {
            patientRepo.Serialize();
            medicalRecordService.MedicalRecordRepo.Serialize();
        }
    }
}
