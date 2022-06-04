using HealthCare_System.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class PatientController
    {
        List<Patient> patients;
        string path;

        public PatientController()
        {
            path = "../../../data/entities/Patients.json";
            Load();
        }

        public PatientController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Patient> Patients { get => patients; set => patients = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            patients = JsonSerializer.Deserialize<List<Patient>>(File.ReadAllText(path));
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

        public void add(Patient patient)
        {
            this.patients.Add(patient);
        }

        public void Serialize()
        {
            string patientsJson = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, patientsJson);
        }
    }
}
