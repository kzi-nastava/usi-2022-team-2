using HealthCare_System.Core.Users.Model;
using System.Text.Json.Serialization;

namespace HealthCare_System.Core.Notifications.Model
{
    public class DaysOffNotification : Notification
    {
        Doctor doctor;
        bool seenByDoctor;

        public DaysOffNotification() { }

        public DaysOffNotification(int id, string message) : base(id, message) { }

        public DaysOffNotification(int id, string message, Doctor doctor) : base(id, message)
        {
            this.doctor = doctor;
            seenByDoctor = false;
        }

        public DaysOffNotification(DaysOffNotification notification) : base(notification.Id, notification.Message)
        {
            doctor = notification.Doctor;
            seenByDoctor = false;
        }

        [JsonIgnore]
        public Doctor Doctor { get => doctor; set => doctor = value; }

        [JsonPropertyName("seenByDoctor")]
        public bool SeenByDoctor { get => seenByDoctor; set => seenByDoctor = value; }

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
