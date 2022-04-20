using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Doctor : Person
    {
        
        List<Appointment> appointments;

        public Doctor()
        {
            this.appointments = new List<Appointment>();
        }

        public Doctor(string jmbg, string firstName, string lastName, DateTime birthDate, string mail, string password, 
            List<Appointment> appointments) : base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
        }

        public Doctor(string jmbg, string firstName, string lastName, DateTime birthDate, string mail, string password) :
            base(jmbg, firstName, lastName, birthDate, mail, password)
        {
            this.appointments = new List<Appointment>();
        }

        public Doctor(Doctor doctor) : base(doctor.Jmbg, doctor.FirstName, doctor.LastName, doctor.BirthDate, doctor.Mail, doctor.Password)
        {
            this.appointments = doctor.appointments;
        }

        [JsonIgnore]
        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }
    
    }
}
