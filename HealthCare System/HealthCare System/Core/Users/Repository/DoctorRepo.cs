using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HealthCare_System.Core.Users.Model;

namespace HealthCare_SystemCore.Core.Users.Repository
{
    public class DoctorRepo
    {
        List<Doctor> doctors;
        string path;

        public DoctorRepo()
        {
            path = "../../../data/entities/Doctors.json";
            Load();
        }

        public DoctorRepo(string path)
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
            List<Doctor> filteredDoctors = new();

            foreach (Doctor doctor in doctors)
            {
                if (doctor.Specialization == specialization)
                    filteredDoctors.Add(doctor);
            }
            return filteredDoctors;
        }
        public List<Doctor> FindByFirstName(string firstName)
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

        public void Serialize()
        {
            string doctorsJson = JsonSerializer.Serialize(doctors, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, doctorsJson);

        }
    }
}
