using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DoctorController
    {
        List<Doctor> doctors;

        public DoctorController()
        {
            Load();
        }

        public List<Doctor> Doctors
        {
            get { return doctors; }
            set { doctors = value; }
        }

        void Load()
        {
            doctors = JsonSerializer.Deserialize<List<Doctor>>(File.ReadAllText("data/entities/Doctors.json"));
        }

        public Doctor FindByMail(string mail)
        {
            foreach (Doctor doctor in doctors)
                if (doctor.Mail == mail)
                    return doctor;
            return null;
        }
        public Doctor FindByJmbg(string jmbg)
        {
            foreach (Doctor doctor in doctors)
                if (doctor.Jmbg == jmbg)
                    return doctor;
            return null;
        }
    }
}
