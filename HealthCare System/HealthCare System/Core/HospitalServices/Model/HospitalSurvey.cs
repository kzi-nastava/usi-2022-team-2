using System.Text.Json.Serialization;

namespace HealthCare_System.Core.HospitalSurveys.Model
{
    public class HospitalSurvey
    {
        int id;
        int serviceQuality;
        int hygiene;
        int satisfaction;
        int recommendation;
        string comment;

        public HospitalSurvey() { }

        public HospitalSurvey(int id, int seviceQuality, int hygiene, int satisfaction, 
            int recommendation, string comment)
        {
            this.id = id;
            this.serviceQuality = seviceQuality;
            this.hygiene = hygiene;
            this.satisfaction = satisfaction;
            this.recommendation = recommendation;
            this.comment = comment;
        }

        public HospitalSurvey(HospitalSurvey hospitalSurvey)
        {
            id = hospitalSurvey.id;
            serviceQuality = hospitalSurvey.serviceQuality;
            hygiene = hospitalSurvey.hygiene;
            satisfaction = hospitalSurvey.satisfaction;
            recommendation = hospitalSurvey.recommendation;
            comment = hospitalSurvey.comment;
        }
        public HospitalSurvey(HospitalSurveyDto hospitalSurvey)
        {
            id = hospitalSurvey.Id;
            serviceQuality = hospitalSurvey.SeviceQuality;
            hygiene = hospitalSurvey.Hygiene;
            satisfaction = hospitalSurvey.Satisfaction;
            recommendation = hospitalSurvey.Recommendation;
            comment = hospitalSurvey.Comment;
        }

        [JsonPropertyName("id")]
        public int Id { get => id; set => id = value; }

        [JsonPropertyName("serviceQuality")]
        public int ServiceQuality { get => serviceQuality; set => serviceQuality = value; }

        [JsonPropertyName("hygiene")]
        public int Hygiene { get => hygiene; set => hygiene = value; }

        [JsonPropertyName("satisfaction")]
        public int Satisfaction { get => satisfaction; set => satisfaction = value; }

        [JsonPropertyName("recommendation")]
        public int Recommendation { get => recommendation; set => recommendation = value; }

        [JsonPropertyName("comment")]
        public string Comment { get => comment; set => comment = value; }

        public override string ToString()
        {
            return "HospitalSurvey[" + "id: " + id + ", serviceQuality: " + serviceQuality 
                + ", hygiene: " + hygiene + ", satisfaction: " + satisfaction + ", recommendation: "
                + recommendation + ", comment: " + comment + "]";
        }

    }
}
