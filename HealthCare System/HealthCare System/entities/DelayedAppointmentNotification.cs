using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class DelayedAppointmentNotification : Notification
    {
        Appointment appointment;

        public DelayedAppointmentNotification() { }

        public DelayedAppointmentNotification(int id, string message) : base(id, message) { }

        public DelayedAppointmentNotification(int id, string message, Appointment appointment) : base(id, message)
        {
            this.appointment = appointment;
        }

        public DelayedAppointmentNotification(DelayedAppointmentNotification notification) : base(notification.Id, notification.Message)
        {
            this.appointment = notification.Appointment;
        }

        [JsonIgnore]
        internal Appointment Appointment { get => appointment; set => appointment = value; }

        public override string ToString()
        {
            int appointmentInfo;
            if (this.appointment is null) appointmentInfo = 91;
            else appointmentInfo = this.appointment.Id;

            return "DrugNotification[" + "id: " + this.Id.ToString() +
                ", message: " + this.Message + ", appointment: " + appointmentInfo.ToString() + "]";
        }
    }

}
