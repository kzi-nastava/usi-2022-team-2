using System;
using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DrugNotification:Notification
    {
        Patient patient;
        Drug drug;
        DateTime time;
        bool seen;

        public DrugNotification() { }

        public DrugNotification(int id, string message) : base(id, message) { }

        public DrugNotification(int id, string message, Patient patient, Drug drug,DateTime time) : base(id, message)
        {
            this.patient = patient;
            this.drug = drug;
            this.time = time;
            seen = false;
        }

        public DrugNotification(DrugNotification notification) : base(notification.Id, notification.Message)
        {
            patient = notification.Patient;
            drug = notification.Drug;
            time = notification.Time;
            seen = notification.seen;
        }

        [JsonIgnore]
        public Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        public Drug Drug { get => drug; set => drug = value; }

        public DateTime Time { get => time; set => time = value; }

        public Boolean Seen { get => seen; set => seen = value; }

        public override string ToString()
        {
            string patientInfo;
            if (patient is null) patientInfo = "null";
            else patientInfo = Patient.Jmbg;

            int drugInfo;
            if (drug is null) drugInfo = -1;
            else drugInfo = drug.Id;

            return "DrugNotification[" + "id: " + Id.ToString()+ ", message: " + Message 
                + ", patient: " + patientInfo + ", appointment: " + drugInfo +", time: " + time.ToString() + ", seen: " + seen.ToString() + "]";
        }
    }
}
