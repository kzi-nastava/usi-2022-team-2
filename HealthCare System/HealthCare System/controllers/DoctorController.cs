﻿using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DoctorController
    {
        List<Doctor> doctors;
        string path;

        public DoctorController()
        {
            path = "data/entities/Doctors.json";
            Load();
        }

        public DoctorController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<Doctor> Doctors { get => doctors; set => doctors = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            doctors = JsonSerializer.Deserialize<List<Doctor>>(File.ReadAllText(path));
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

        public void Serialize()
        {
            string doctorsJson = JsonSerializer.Serialize(doctors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, doctorsJson);
        }
    }
}
