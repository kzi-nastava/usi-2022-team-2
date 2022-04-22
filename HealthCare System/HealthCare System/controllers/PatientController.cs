using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class PatientController
    {
        List<Patient> patients;

        public PatientController()
        {
            Load();
        }

        public List<Patient> Patients
        {
            get { return patients; }
            set { patients = value; }
        }

        void Load()
        {
            patients = JsonSerializer.Deserialize<List<Patient>>(File.ReadAllText("data/entities/Patients.json"));
        }

        public Patient FindByMail(string mail)
        {
            foreach (Patient patient in patients)
                if (patient.Mail == mail)
                    return patient;
            return null;
        }
        public Patient FindByJmbg(string jmbg)
        {
            foreach (Patient patient in patients)
                if (patient.Jmbg == jmbg)
                    return patient;
            return null;
        }

    }
}
