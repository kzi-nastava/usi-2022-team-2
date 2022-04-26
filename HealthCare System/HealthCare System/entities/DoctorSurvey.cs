using System.Text.Json.Serialization;

namespace HealthCare_System.entities
{
    public class DoctorSurvey
    {
        int id;
        Doctor doctor;
        int serviceQuality;
        int recommendation;
        string comment;

        public DoctorSurvey() { }

        public DoctorSurvey(int id, Doctor doctor, int serviceQuality, int recommendation, string comment)
        {
            this.id = id;
            this.doctor = doctor;
            this.serviceQuality = serviceQuality;
            this.recommendation = recommendation;
            this.comment = comment;
        }

        public DoctorSurvey(DoctorSurvey doctorSurvey)
        {
            id = doctorSurvey.id;
            doctor = doctorSurvey.doctor;
            serviceQuality = doctorSurvey.serviceQuality;
            recommendation = doctorSurvey.recommendation;
            comment = doctorSurvey.comment;
        }

        public DoctorSurvey(int id, int serviceQuality, int recommendation, string comment)
        {
            this.id = id;
            this.serviceQuality = serviceQuality;
            this.recommendation = recommendation;
            this.comment = comment;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("serviceQuality")]
        public int ServiceQuality { get => serviceQuality; set => serviceQuality = value; }

        [JsonPropertyName("recommendation")]
        public int Recommendation { get => recommendation; set => recommendation = value; }

        [JsonPropertyName("comment")]
        public string Comment { get => comment; set => comment = value; }

        [JsonIgnore]
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public override string ToString()
        {
            string doctorInfo;
            if (doctor is null) doctorInfo = "-1";
            else doctorInfo = Doctor.Jmbg;

            return "DoctorSurvey[" + "id: " + id + ", doctor: " + doctorInfo + ", serviceQuality: " + serviceQuality +
                ", recommendation: " + recommendation + ", comment: " + comment + "]";
        }
    }
}
