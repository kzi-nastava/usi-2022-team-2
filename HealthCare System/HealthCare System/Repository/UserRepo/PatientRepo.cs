using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Repository.UserRepo
{
    class PatientRepo
    {
        List<Patient> patients;
        string path;

        public PatientRepo()
        {
            path = "../../../data/entities/Patients.json";
            Load();
        }

        public PatientRepo(string path)
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

        public void Add(Patient patient)
        {
            patients.Add(patient);
            Serialize();
        }
        public void Delete(Patient patient)
        {
            Patients.Remove(patient);
            Serialize();
        }
        

        public void Serialize()
        {
            string patientsJson = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, patientsJson);
        }
    }
}
