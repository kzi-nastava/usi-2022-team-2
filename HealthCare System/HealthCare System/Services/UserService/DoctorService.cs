using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.UserRepo;

namespace HealthCare_System.Services.UserService
{
    class DoctorService
    {
        DoctorRepo doctorRepo;

        public DoctorService()
        {
            DoctorRepoFactory doctorRepoFactory = new();
            doctorRepo = doctorRepoFactory.CreateDoctorRepository();
        }

        public DoctorRepo DoctorRepo { get => doctorRepo; }

        public List<Doctor> FilterDoctors(string firstName, string lastName, Specialization specialization)
        {
            List<Doctor> filterFirstName = doctors;
            List<Doctor> filterLastName = doctors;
            List<Doctor> filterSpecialization = doctors;

            if (firstName.Length >= 3)
                filterFirstName = FindByFirstName(firstName);
            if (lastName.Length >= 3)
                filterLastName = FindByLastName(lastName);
            if (specialization != Specialization.NULL)
                filterSpecialization = FindBySpecialization(specialization);

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
                sortedDoctors = SortDoctorsByRatings(doctors, direction);
            else if (priority == DoctorSortPriority.FIRST_NAME)
                sortedDoctors = doctorController.SortDoctorsByFirstName(doctors, direction);
            else if (priority == DoctorSortPriority.LAST_NAME)
                sortedDoctors = doctorController.SortDoctorsByLastName(doctors, direction);
            else
                sortedDoctors = doctorController.SortDoctorsBySpecialization(doctors, direction);
            return sortedDoctors;
        }
    }
}
