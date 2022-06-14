using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.Users
{
    class DoctorController
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        public List<Doctor> Doctors()
        {
            return doctorService.Doctors();
        }

        public List<Doctor> FilterDoctors(string firstName, string lastName, Specialization specialization)
        {
            return doctorService.FilterDoctors(firstName, lastName, specialization);
        }

        public List<Doctor> SortDoctorsByFirstName(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            return doctorService.SortDoctorsByFirstName(unsortedDoctors, direction);
        }

        public List<Doctor> SortDoctorsByLastName(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            return doctorService.SortDoctorsByLastName(unsortedDoctors, direction);
        }

        public List<Doctor> SortDoctorsBySpecialization(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            return doctorService.SortDoctorsBySpecialization(unsortedDoctors, direction);
        }

        public List<Doctor> SortDoctors(List<Doctor> doctors, DoctorSortPriority priority, SortDirection direction)
        {
            return doctorService.SortDoctors(doctors, priority, direction);
        }

        public Doctor FindByMail(string mail)
        {
            return doctorService.FindByMail(mail);
        }
        public Doctor FindByJmbg(string jmbg)
        {
            return doctorService.FindByJmbg(jmbg);
        }

        public List<Doctor> FindBySpecialization(Specialization specialization)
        {
            return doctorService.FindBySpecialization(specialization);
        }
        public List<Doctor> FindByFirstName(string firstName)
        {
            return doctorService.FindByFirstName(firstName);
        }
        public List<Doctor> FindByLastName(string lastName)
        {
            return doctorService.FindByLastName(lastName);
        }

        public void Serialize()
        {
            doctorService.Serialize();
        }
    }
}
