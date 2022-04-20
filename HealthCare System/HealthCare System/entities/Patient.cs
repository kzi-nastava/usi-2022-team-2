using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Patient : Person
    {
        List<Appointment> appointments;
        bool blocked;

        public Patient()
        {
            this.appointments = new List<Appointment>();
        }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password, List<Appointment> appointments, bool blocked) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
            this.blocked = blocked;
        }

        public Patient(string jmbg, string firstName, string lastName, DateTime birthDate, string mail,
            string password,bool blocked) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.appointments = new List<Appointment>();
            this.blocked = blocked;

        }

        public Patient(Patient patient) : base(patient.Jmbg, patient.FirstName, patient.LastName, patient.BirthDate, patient.Mail, patient.Password)
        {
            this.appointments = patient.appointments;
            this.blocked = patient.blocked;
        }

        [JsonIgnore]
        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

        [JsonPropertyName("blocked")]
        public bool Blocked
        {
            get { return blocked; }
            set { blocked = value; }
        }

        public List<Appointment> UpcomingAppointment(int nextDays)
        {
            return null;
        }
    }
}
