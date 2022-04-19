using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class Doctor : Person
    {
        List<Appointment> appointments;

        public Doctor()
        {
        }

        public Doctor(string firstName, string lastName, DateTime birthDate, string mail, string password, 
            List<Appointment> appointments) : base(firstName, lastName, birthDate, mail, password)
        {
            this.appointments = appointments;
        }

        public Doctor(string firstName, string lastName, DateTime birthDate, string mail, string password) :
            base(firstName, lastName, birthDate, mail, password)
        {
            this.appointments = null;
        }

        public Doctor(Doctor doctor) : base(doctor.FirstName, doctor.LastName, doctor.BirthDate, doctor.Mail, doctor.Password)
        {
            this.appointments = doctor.appointments;
        }

        public List<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }
    
    }
}
