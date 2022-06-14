using HealthCare_System.Core.HospitalSurveys.Model;
using HealthCare_System.Core.HospitalSurveys.Repository;
using System.Collections.Generic;

namespace HealthCare_System.Core.HospitalSurveys
{
    public interface IHospitalSurveyService
    {
        IHospitalSurveyRepo HospitalSurveyRepo { get; }

        void Add(HospitalSurveyDto hospitalSurveyDto);

        List<HospitalSurvey> HospitalSurveys();

        int GenerateId();

        HospitalSurvey FindById(int id);

        void Serialize();
    }
}