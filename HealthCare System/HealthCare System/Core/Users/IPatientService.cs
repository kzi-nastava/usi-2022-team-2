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
    public interface IPatientService
    {
        IPatientRepo PatientRepo { get; }
        public ISchedulingService SchedulingService { get ; set ; }
        public IPrescriptionService PrescriptionService { get ; set ; }
        public IMedicalRecordService MedicalRecordService { get ; set ; }
        public IIngredientService IngredientService { get ; set ; }

        void AddPatient(PersonDto personDto, MedicalRecord medRecord);
        void BlockPatient(Patient patient);
        void DeletePatient(Patient patient);
        List<Patient> Patients();
        void UpdatePatient();
        public Patient FindByMail(string mail);

        public Patient FindByJmbg(string jmbg);

        public void Serialize();
    }
}