using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DaysOffNotification : Notification
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
            doctor = notification.Doctor;
        }

        [JsonIgnore]
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (doctor is null) doctorInfo = "null";
            else doctorInfo = doctor.Jmbg;

            return "DrugNotification[" + "id: " + Id.ToString() +
                ", message: " + Message + ", doctor: " + doctorInfo + "]";
        }
    }
}
