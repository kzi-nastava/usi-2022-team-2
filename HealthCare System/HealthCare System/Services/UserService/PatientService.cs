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

        public void DeletePatient(Patient patient)
        {
            MedicalRecord medicalRecord = patient.MedicalRecord;

            try
            {
                DeleteAppointmens(patient);
            }
            catch
            {
                throw;
            }
            DeletePrescriptions(medicalRecord);

            medicalRecordController.MedicalRecords.Remove(medicalRecord);
            medicalRecordController.Serialize();

            patientController.Patients.Remove(patient);
            patientController.Serialize();

        }
        public void AddPatient(Patient patient, MedicalRecord medRecord)
        {
            patientController.Patients.Add(patient);

            patient.MedicalRecord = medRecord;
            medRecord.Patient = patient;

            patientController.Serialize();
            medicalRecordController.Serialize();
            ingredientController.Serialize();
        }
        public void UpdatePatient()
        {
            patientController.Serialize();
            medicalRecordController.Serialize();
        }
    }
}
