using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    public enum AppointmentType{ EXAMINATION, OPERATION }
    class Appointment
    {
        DateTime start;
        DateTime end;
        Doctor doctor;
        Patient patient;
        AppointmentType type;

        public Appointment()
        {

        }
        public Appointment(DateTime start, DateTime end, Doctor doctor, Patient patient, AppointmentType type)
        {
            this.start = start;
            this.end = end;
            this.doctor = doctor;
            this.patient = patient;
            this.type = type;
        }

        public Appointment(DateTime start, DateTime end, AppointmentType type)
        {
            this.start = start;
            this.end = end;
            this.type = type;
            this.doctor = null;
            this.patient = null;

        }
        public Appointment(Appointment appointment)
        {
            this.start = appointment.start;
            this.end = appointment.end;
            this.doctor = appointment.doctor;
            this.patient = appointment.patient;
            this.type = appointment.type;
        }

        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }
        public Doctor Doctor //??
        {
            get { return doctor; }
            set { doctor = value; }
        }
        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }
        public AppointmentType Type
        {
            get { return type; }
            set { type = value; }
        }



    }
}
