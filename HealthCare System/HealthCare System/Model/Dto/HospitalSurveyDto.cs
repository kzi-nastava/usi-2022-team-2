using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class HospitalSurveyDto
    {
        int id;
        int seviceQuality;
        int hygiene;
        int satisfaction;
        int recommendation;
        string comment;

        public HospitalSurveyDto(int id, int seviceQuality, int hygiene, int satisfaction, int recommendation, string comment)
        {
            this.id = id;
            this.seviceQuality = seviceQuality;
            this.hygiene = hygiene;
            this.satisfaction = satisfaction;
            this.recommendation = recommendation;
            this.comment = comment;
        }

        public int Id { get => id; set => id = value; }
        public int SeviceQuality { get => seviceQuality; set => seviceQuality = value; }
        public int Hygiene { get => hygiene; set => hygiene = value; }
        public int Satisfaction { get => satisfaction; set => satisfaction = value; }
        public int Recommendation { get => recommendation; set => recommendation = value; }
        public string Comment { get => comment; set => comment = value; }
    }
}
