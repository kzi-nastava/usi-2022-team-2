using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public interface IAppointmentService
    {
        List<Appointment> Appointments();
        List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium, SortDirection direction);
        List<Appointment> SortByDate(List<Appointment> unsortedAppointments, SortDirection direction);
        List<Appointment> SortByDoctor(List<Appointment> unsortedAppointments, SortDirection direction);
        List<Appointment> SortBySpecialization(List<Appointment> unsortedAppointments, SortDirection direction);
    }
}