using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Model.Dto
{
    public class DoctorSurveyDto
    {
        int id;
        Doctor doctor;
        int serviceQuality;
        int recommendation;
        string comment;

        public DoctorSurveyDto(int id, Doctor doctor, int serviceQuality, int recommendation, string comment)
        {
            this.id = id;
            this.doctor = doctor;
            this.serviceQuality = serviceQuality;
            this.recommendation = recommendation;
            this.comment = comment;
        }

        public int Id { get => id; set => id = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public int ServiceQuality { get => serviceQuality; set => serviceQuality = value; }
        public int Recommendation { get => recommendation; set => recommendation = value; }
        public string Comment { get => comment; set => comment = value; }
    }
}
