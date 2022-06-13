using HealthCare_System.Model.Dto;
using HealthCare_System.Model;
using HealthCare_System.Repository.SurveyRepo;

namespace HealthCare_System.Core.HospitalSurveys
{
    public class HospitalSurveyService
    {
        HospitalSurveyRepo hospitalSurveyRepo;

        public HospitalSurveyService(HospitalSurveyRepo hospitalSurveyRepo)
        {
            this.hospitalSurveyRepo = hospitalSurveyRepo;
        }

        public HospitalSurveyRepo HospitalSurveyRepo { get => hospitalSurveyRepo; }

        public void Add (HospitalSurveyDto hospitalSurveyDto)
        {
            HospitalSurvey hospitalSurvey = new(hospitalSurveyDto);
            hospitalSurveyRepo.Add(hospitalSurvey);
        }
    }
}
