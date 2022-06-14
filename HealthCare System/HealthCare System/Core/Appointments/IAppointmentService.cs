using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Appointments.Repository;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public interface IAppointmentService
    {
        List<Appointment> Appointments();

        IAppointmentRepo AppointmentRepo { get; }
        List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium, SortDirection direction);
        List<Appointment> SortByDate(List<Appointment> unsortedAppointments, SortDirection direction);
        List<Appointment> SortByDoctor(List<Appointment> unsortedAppointments, SortDirection direction);
        List<Appointment> SortBySpecialization(List<Appointment> unsortedAppointments, SortDirection direction);
        public Appointment FindById(int id);

        public int GenerateId();

        public void Serialize(string linkPath = "../../../data/links/AppointmentLinker.csv");
        public List<Appointment> FindByWord(Patient patient, string word);

        public List<Appointment> FindPastAppointments(Patient patient);

        public List<Appointment> FindUpcomingAppointments(Patient patient);
        public void Add(Appointment appointment);
    }
}