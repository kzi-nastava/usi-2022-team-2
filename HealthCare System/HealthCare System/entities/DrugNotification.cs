using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    class DrugNotification:Notification
    {
        Patient patient;
        Drug drug;

        public DrugNotification() { }

        public DrugNotification(int id, string message) : base(id, message)
        {

        }

        public DrugNotification(int id, string message, Patient patient, Drug drug) : base(id, message)
        {
            this.patient = patient;
            this.drug = drug;
        }

        public DrugNotification(DrugNotification notification) : base(notification.Id, notification.Message)
        {
            this.patient = notification.Patient;
            this.drug = notification.Drug;
        }

        [JsonIgnore]
        internal Patient Patient { get => patient; set => patient = value; }

        [JsonIgnore]
        internal Drug Drug { get => drug; set => drug = value; }

        public override string ToString()
        {
            string patientInfo;
            if (this.patient is null) patientInfo = "null";
            else patientInfo = this.Patient.Jmbg;

            int drugInfo;
            if (this.drug is null) drugInfo = -1;
            else drugInfo = this.drug.Id;

            return "DrugNotification[" + "id: " + this.Id.ToString()+
                ", message: " + this.Message + ", patient: " + patientInfo +
                ", appointment: " + drugInfo + "]";
        }
    }
}
