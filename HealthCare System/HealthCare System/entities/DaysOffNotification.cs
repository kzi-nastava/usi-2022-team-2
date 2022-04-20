using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare_System.entities
{
    class DaysOffNotification : Notification
    {
        Doctor doctor;

        public DaysOffNotification() { }

        public DaysOffNotification(int id, string message) : base(id, message) { }

        public DaysOffNotification(int id, string message, Doctor doctor) : base(id, message)
        {
            this.doctor = doctor;
        }

        public DaysOffNotification(DaysOffNotification notification) : base(notification.Id, notification.Message)
        {
            this.doctor = notification.Doctor;
        }

        [JsonIgnore]
        internal Doctor Doctor { get => doctor; set => doctor = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (this.doctor is null) doctorInfo = "null";
            else doctorInfo = this.doctor.Jmbg;

            return "DrugNotification[" + "id: " + this.Id.ToString() +
                ", message: " + this.Message + ", doctor: " + doctorInfo + "]";
        }
    }
}
