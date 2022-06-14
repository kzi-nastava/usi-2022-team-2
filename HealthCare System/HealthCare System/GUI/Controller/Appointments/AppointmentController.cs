using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller.Appointments
{
    class AppointmentController
    {
        private readonly AppointmentService appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public List<Appointment> Appointments()
        {
            return appointmentService.Appointments();
        }

        public List<Appointment> SortByDate(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            return appointmentService.SortByDate(unsortedAppointments, direction);

        }

        public List<Appointment> SortByDoctor(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            return appointmentService.SortByDoctor(unsortedAppointments, direction);

        }

        public List<Appointment> SortBySpecialization(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            return appointmentService.SortBySpecialization(unsortedAppointments, direction);

        }

        public List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium,
            SortDirection direction)
        {
            return appointmentService.SortAnamneses(patient, word, criterium, direction);
        }

        private void DeleteAppointmens(Patient patient)
        {
            appointmentService.DeleteAppointmens(patient);
        }
        public Appointment FindById(int id)
        {
            return appointmentService.FindById(id);
        }

        public int GenerateId()
        {
            return appointmentService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/AppointmentLinker.csv")
        {
            appointmentService.Serialize(linkPath);
        }
        public List<Appointment> FindByWord(Patient patient, string word)
        {
            return appointmentService.FindByWord(patient, word);
        }

        public List<Appointment> FindPastAppointments(Patient patient)
        {
            return appointmentService.FindPastAppointments(patient);
        }

        public List<Appointment> FindUpcomingAppointments(Patient patient)
        {
            return appointmentService.FindUpcomingAppointments(patient);
        }
    }
}
