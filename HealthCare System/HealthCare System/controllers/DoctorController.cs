using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace HealthCare_System.controllers
{
    class DoctorController
    {
        List<Doctor> doctors;
        string path;

        public DoctorController()
        {
            path = "../../../data/entities/Doctors.json";
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

        public List<Doctor> FindBySpecialization(Specialization specialization)
        {
            List<Doctor> filteredDoctors = new ();

            foreach (Doctor doctor in doctors)
            {
                if (doctor.Specialization == specialization)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }
        public List<Doctor>FindByFirstName(string firstName)
        {
            List<Doctor> filteredDoctors = new();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.FirstName.ToLower().Contains(firstName.ToLower()))
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }
        public List<Doctor> FindByLastName(string lastName)
        {
            List<Doctor> filteredDoctors = new();
            foreach (Doctor doctor in doctors)
            {
                if (doctor.LastName.ToLower().Contains(lastName.ToLower()))
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }
        public List<Doctor>FilterDoctors(string firstName,string lastName,Specialization specialization)
        {
            List<Doctor> filterFirstName = doctors;
            List<Doctor> filterLastName = doctors;
            List<Doctor> filterSpecialization = doctors;

            if (firstName.Length>=3)
                filterFirstName = FindByFirstName(firstName);
            if(lastName.Length >= 3)
                filterLastName = FindByLastName(lastName);
            if (specialization!=Specialization.NULL)
                filterSpecialization = FindBySpecialization(specialization);

            return filterFirstName.Intersect(filterLastName).Intersect(filterSpecialization).ToList();

        }
        public List<Doctor>SortDoctorsByFirstName(List<Doctor> unsortedDoctors,SortDirection direction)
        {
            if (direction==SortDirection.DESCENDING)
                return unsortedDoctors.OrderByDescending(t => t.FirstName).ToList();
            else
                return unsortedDoctors.OrderBy(t => t.FirstName).ToList();
        }
        public List<Doctor> SortDoctorsByLastName(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            if (direction == SortDirection.DESCENDING)
                return unsortedDoctors.OrderByDescending(t => t.LastName).ToList();
            else
                return unsortedDoctors.OrderBy(t => t.LastName).ToList();
        }
        public List<Doctor> SortDoctorsBySpecialization(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            if (direction == SortDirection.DESCENDING)
                return unsortedDoctors.OrderByDescending(t => t.Specialization).ToList();
            else
                return unsortedDoctors.OrderBy(t => t.Specialization).ToList();
        }

        public void Serialize()
        {
            string doctorsJson = JsonSerializer.Serialize(doctors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, doctorsJson);

        }
    }
}
