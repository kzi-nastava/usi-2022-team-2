using HealthCare_System.Core.HospitalSurveys.Model;
using HealthCare_System.Core.HospitalSurveys.Repository;

namespace HealthCare_System.Core.HospitalSurveys
{
    public class HospitalSurveyService : IHospitalSurveyService
    {
        IHospitalSurveyRepo hospitalSurveyRepo;

        public HospitalSurveyService(IHospitalSurveyRepo hospitalSurveyRepo)
        {
            this.hospitalSurveyRepo = hospitalSurveyRepo;
        }

        public IHospitalSurveyRepo HospitalSurveyRepo { get => hospitalSurveyRepo; }

        public void Add(HospitalSurveyDto hospitalSurveyDto)
        {
            HospitalSurvey hospitalSurvey = new(hospitalSurveyDto);
            hospitalSurveyRepo.Add(hospitalSurvey);
        }
    }
}
