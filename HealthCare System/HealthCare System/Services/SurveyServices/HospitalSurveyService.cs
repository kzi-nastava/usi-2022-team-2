using HealthCare_System.Repository.SurveyRepo;

namespace HealthCare_System.Services.SurveyServices
{
    class HospitalSurveyService
    {
        HospitalSurveyRepo hospitalSurveyRepo;

        public HospitalSurveyService(HospitalSurveyRepo hospitalSurveyRepo)
        {
            this.hospitalSurveyRepo = hospitalSurveyRepo;
        }

        public HospitalSurveyRepo HospitalSurveyRepo { get => hospitalSurveyRepo; }
    }
}
