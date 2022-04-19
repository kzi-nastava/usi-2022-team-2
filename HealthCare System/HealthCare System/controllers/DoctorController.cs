using HealthCare_System.entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            this.doctors = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText("data/entities/Doctors.json"));
        }

        public Doctor FindByMail(string mail)
        {
            foreach (Doctor doctor in this.doctors)
                if (doctor.Mail == mail)
                    return doctor;
            return null;
        }
    }
}
