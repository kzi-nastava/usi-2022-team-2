using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public interface IPatientService
    {
        void AddPatient(PersonDto personDto, MedicalRecord medRecord);
        void BlockPatient(Patient patient);
        void DeletePatient(Patient patient);
        List<Patient> Patients();
        void UpdatePatient();
    }
}