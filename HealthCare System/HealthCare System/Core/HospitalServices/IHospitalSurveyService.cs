using HealthCare_System.Core.HospitalSurveys.Model;
using HealthCare_System.Core.HospitalSurveys.Repository;

namespace HealthCare_System.Core.HospitalSurveys
{
    public interface IHospitalSurveyService
    {
        IHospitalSurveyRepo HospitalSurveyRepo { get; }

        void Add(HospitalSurveyDto hospitalSurveyDto);
    }
}