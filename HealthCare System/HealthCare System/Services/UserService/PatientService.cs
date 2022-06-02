using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.AppointmentService;
using HealthCare_System.Services.PrescriptionService;
using HealthCare_System.Services.MedicalRecordService;

namespace HealthCare_System.Services.UserService
{
    class PatientService
    {
        PatientRepo patientRepo;
        SchedulingService.SchedulingService schedulingService;
        PrescriptionService.PrescriptionService prescriptionService;
        MedicalRecordService.MedicalRecordService medicalRecordService;
        IngredientService.IngredientService ingredientService;
        
        public PatientService()
        {
            PatientRepoFactory patientRepoFactory = new();
            patientRepo = patientRepoFactory.CreatePatientRepository();
        }

        public PatientRepo PatientRepo { get => patientRepo;}

        public List<Patient> Patients()
        {
            return patientRepo.Patients;
        }

        public void DeletePatient(Patient patient)
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;
            schedulingService = new();
            prescriptionService = new();
            medicalRecordService = new();
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
            medicalRecordService = new();
            ingredientService = new();
            patientRepo.Add(patient);

            patient.MedicalRecord = medRecord;
            medRecord.Patient = patient;
            medicalRecordService.MedicalRecordRepo.Serialize();
            ingredientService.IngredientRepo.Serialize();
        }
        public void UpdatePatient()
        {
            medicalRecordService = new();
            patientRepo.Serialize();
            medicalRecordService.MedicalRecordRepo.Serialize();
        }
    }
}
