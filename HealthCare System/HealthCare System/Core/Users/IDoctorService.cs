using HealthCare_System.Core.DoctorSurveys;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public interface IDoctorService
    {
        IDoctorRepo DoctorRepo { get; }
        public IDoctorSurveyService SurveyService { get ; set ; }

        List<Doctor> Doctors();

        List<Doctor> FilterDoctors(string firstName, string lastName, Specialization specialization);

        List<Doctor> SortDoctors(List<Doctor> doctors, DoctorSortPriority priority, SortDirection direction);

        List<Doctor> SortDoctorsByFirstName(List<Doctor> unsortedDoctors, SortDirection direction);

        List<Doctor> SortDoctorsByLastName(List<Doctor> unsortedDoctors, SortDirection direction);

        List<Doctor> SortDoctorsBySpecialization(List<Doctor> unsortedDoctors, SortDirection direction);

        public Doctor FindByMail(string mail);

        public Doctor FindByJmbg(string jmbg);

        public List<Doctor> FindBySpecialization(Specialization specialization);

        public List<Doctor> FindByFirstName(string firstName);

        public List<Doctor> FindByLastName(string lastName);

        public void Serialize();

    }
}