using HealthCare_System.Core.DoctorSurveys;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.Users.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare_System.Core.Users
{
    public class DoctorService : IDoctorService
    {
        DoctorRepo doctorRepo;
        DoctorSurveyService surveyService;

        public DoctorService(DoctorRepo doctorRepo, DoctorSurveyService surveyService)
        {
            this.doctorRepo = doctorRepo;
            this.surveyService = surveyService;
        }

        public DoctorRepo DoctorRepo { get => doctorRepo; }

        public List<Doctor> Doctors()
        {
            return doctorRepo.Doctors;
        }

        public List<Doctor> FilterDoctors(string firstName, string lastName, Specialization specialization)
        {
            List<Doctor> filterFirstName = doctorRepo.Doctors;
            List<Doctor> filterLastName = doctorRepo.Doctors;
            List<Doctor> filterSpecialization = doctorRepo.Doctors;

            if (firstName.Length >= 3)
                filterFirstName = DoctorRepo.FindByFirstName(firstName);
            if (lastName.Length >= 3)
                filterLastName = DoctorRepo.FindByLastName(lastName);
            if (specialization != Specialization.NULL)
                filterSpecialization = DoctorRepo.FindBySpecialization(specialization);

            return filterFirstName.Intersect(filterLastName).Intersect(filterSpecialization).ToList();

        }

        public List<Doctor> SortDoctorsByFirstName(List<Doctor> unsortedDoctors, SortDirection direction)
        {
            if (direction == SortDirection.DESCENDING)
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

        public List<Doctor> SortDoctors(List<Doctor> doctors, DoctorSortPriority priority, SortDirection direction)
        {
            List<Doctor> sortedDoctors = new();
            if (priority == DoctorSortPriority.RATINGS)
                sortedDoctors = surveyService.SortDoctorsByRatings(doctors, direction);
            else if (priority == DoctorSortPriority.FIRST_NAME)
                sortedDoctors = SortDoctorsByFirstName(doctors, direction);
            else if (priority == DoctorSortPriority.LAST_NAME)
                sortedDoctors = SortDoctorsByLastName(doctors, direction);
            else
                sortedDoctors = SortDoctorsBySpecialization(doctors, direction);
            return sortedDoctors;
        }
    }
}
