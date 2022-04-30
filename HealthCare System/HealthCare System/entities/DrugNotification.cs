using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DrugNotification:Notification
    {
        Patient patient;
        Drug drug;

        public DrugNotification() { }

        public DrugNotification(int id, string message) : base(id, message) { }

        public DrugNotification(int id, string message, Patient patient, Drug drug) : base(id, message)
        {
            this.patient = patient;
            this.drug = drug;
        }

        public DrugNotification(DrugNotification notification) : base(notification.Id, notification.Message)
        {
            patient = notification.Patient;
            drug = notification.Drug;
        }

        [JsonIgnore]
        public Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        public Drug Drug { get => drug; set => drug = value; }

        public override string ToString()
        {
            string patientInfo;
            if (patient is null) patientInfo = "null";
            else patientInfo = Patient.Jmbg;

            int drugInfo;
            if (drug is null) drugInfo = -1;
            else drugInfo = drug.Id;

            return "DrugNotification[" + "id: " + Id.ToString()+ ", message: " + Message 
                + ", patient: " + patientInfo + ", appointment: " + drugInfo + "]";
        }
    }
}
