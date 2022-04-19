using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthCare_System.controllers
{
    class DoctorController
    {
        List<Doctor> doctors;

        public DoctorController()
        {
            this.LoadDoctors();
        }

        public List<Doctor> Doctors
        {
            get { return doctors; }
            set { doctors = value; }
        }

        void LoadDoctors()
        {
            this.doctors = JsonSerializer.Deserialize<List<Doctor>>(File.ReadAllText("data/entities/Doctors.json"));
        }

        public Doctor FindByMail(string mail)
        {
            foreach (Doctor doctor in this.doctors)
                if (doctor.Mail == mail)
                    return doctor;
            return null;
        }
        public Doctor FindByJmbg(string jmbg)
        {
            foreach (Doctor doctor in this.doctors)
                if (doctor.Jmbg == jmbg)
                    return doctor;
            return null;
        }
    }
}
