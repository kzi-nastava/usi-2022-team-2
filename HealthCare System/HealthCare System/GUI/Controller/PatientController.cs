using HealthCare_System.Core.MedicalRecords.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class PatientController
    {
        IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        public List<Patient> Patients()
        {
            return patientService.Patients();
        }

        public void DeletePatient(Patient patient)
        {
            patientService.DeletePatient(patient);
        }
        public void AddPatient(PersonDto personDto, MedicalRecord medRecord)
        {
            patientService.AddPatient(personDto, medRecord);
        }
        public void UpdatePatient()
        {
            patientService.UpdatePatient();
        }

        public void BlockPatient(Patient patient)
        {
            patientService.BlockPatient(patient);
        }

        public Patient FindByMail(string mail)
        {
            return patientService.FindByMail(mail);
        }

        public Patient FindByJmbg(string jmbg)
        {
            return patientService.FindByJmbg(jmbg);
        }

        public void Serialize()
        {
            patientService.Serialize();
        }
    }
}
