using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Patient : Person
    {
        List<Appointment> appointments;

        public Patient()
        {
            this.appointments = new List<Appointment>();
        }

        public Patient(string firstName, string lastName, DateTime birthDate, string mail,
            string password, List<Appointment> appointments) : base(firstName, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
        }

        public Patient(string firstName, string lastName, DateTime birthDate, string mail,
            string password) : base(firstName, lastName, birthDate, mail, password)
        {
            this.appointments = new List<Appointment>();
        }

        public Patient(Patient patient) : base(patient.FirstName, patient.LastName, patient.BirthDate, patient.Mail, patient.Password)
        {
            this.appointments = patient.appointments;
        }

        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

        public List<Appointment> UpcomingAppointment(int nextDays)
        {
            return null;
        }
    }
}
