using HealthCare_System.entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class PatientController
    {
        List<Patient> patients;

        public PatientController()
        {
            this.LoadPatients();
        }

        public List<Patient> Patients
        {
            get { return patients; }
            set { patients = value; }
        }

        void LoadPatients()
        {
            this.patients = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText("data/entities/Patients.json"));
        }

        public Patient FindByMail(string mail)
        {
            foreach (Patient patient in this.patients)
                if (patient.Mail == mail)
                    return patient;
            return null;
        }

    }
}
