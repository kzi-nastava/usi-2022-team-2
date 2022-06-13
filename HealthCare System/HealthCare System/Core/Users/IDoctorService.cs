using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Users
{
    public interface IDoctorService
    {
        List<Doctor> Doctors();
        List<Doctor> FilterDoctors(string firstName, string lastName, Specialization specialization);
        List<Doctor> SortDoctors(List<Doctor> doctors, DoctorSortPriority priority, SortDirection direction);
        List<Doctor> SortDoctorsByFirstName(List<Doctor> unsortedDoctors, SortDirection direction);
        List<Doctor> SortDoctorsByLastName(List<Doctor> unsortedDoctors, SortDirection direction);
        List<Doctor> SortDoctorsBySpecialization(List<Doctor> unsortedDoctors, SortDirection direction);
    }
}