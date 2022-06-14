using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Ingredients;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Prescriptions;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public class PatientService : IPatientService
    {
        IPatientRepo patientRepo;
        ISchedulingService schedulingService;
        IPrescriptionService prescriptionService;
        IMedicalRecordService medicalRecordService;
        IIngredientService ingredientService;

        public PatientService(IPatientRepo patientRepo, ISchedulingService schedulingService,
            IPrescriptionService prescriptionService, IMedicalRecordService medicalRecordService,
            IIngredientService ingredientService)
        {
            this.patientRepo = patientRepo;
            this.schedulingService = schedulingService;
            this.prescriptionService = prescriptionService;
            this.medicalRecordService = medicalRecordService;
            this.ingredientService = ingredientService;
        }

        public IPatientRepo PatientRepo { get => patientRepo; }

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
        public void AddPatient(PersonDto personDto, MedicalRecord medRecord)
        {
            Patient patient = new();
            patient.Jmbg = personDto.Jmbg;
            patient.FirstName = personDto.FirstName;
            patient.LastName = personDto.LastName;
            patient.Mail = personDto.Mail;
            patient.BirthDate = personDto.BirthDate;
            patient.Password = personDto.Password;
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

        public void BlockPatient(Patient patient)
        {
            patient.Blocked = !patient.Blocked;
            patientRepo.Serialize();
        }
    }
}
