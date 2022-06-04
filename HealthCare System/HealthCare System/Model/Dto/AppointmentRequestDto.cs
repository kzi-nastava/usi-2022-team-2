using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class AppointmentRequestDto
    {
        int id;
        AppointmentState state;
        Patient patient;
        Appointment oldAppointment;
        Appointment newAppointment;
        RequestType type;
        DateTime requestCreated;

        public AppointmentRequestDto(int id, AppointmentState state, Patient patient, Appointment oldAppointment, Appointment newAppointment, RequestType type, DateTime requestCreated)
        {
            this.id = id;
            this.state = state;
            this.patient = patient;
            this.oldAppointment = oldAppointment;
            this.newAppointment = newAppointment;
            this.type = type;
            this.requestCreated = requestCreated;
        }

        public int Id { get => id; set => id = value; }
        public AppointmentState State { get => state; set => state = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Appointment OldAppointment { get => oldAppointment; set => oldAppointment = value; }
        public Appointment NewAppointment { get => newAppointment; set => newAppointment = value; }
        public RequestType Type { get => type; set => type = value; }
        public DateTime RequestCreated { get => requestCreated; set => requestCreated = value; }
    }
}
