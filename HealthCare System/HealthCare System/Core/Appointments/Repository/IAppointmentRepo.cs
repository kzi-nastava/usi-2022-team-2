using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments.Repository
{
    public interface IAppointmentRepo
    {
        string Path { get; set; }

        List<Appointment> Appointments { get; set; }

        Appointment FindById(int id);
        List<Appointment> FindByWord(Patient patient, string word);
        List<Appointment> FindPastAppointments(Patient patient);
        List<Appointment> FindUpcomingAppointments(Patient patient);
        int GenerateId();
        void Serialize(string linkPath = "../../../data/links/AppointmentLinker.csv");
        public void Add(Appointment appointment);
    }
}