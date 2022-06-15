using HealthCare_System.Core.HospitalSurveys;
using HealthCare_System.Core.HospitalSurveys.Model;
using System.Collections.Generic;

namespace HealthCare_System.GUI.Controller.HospitalServices
{
    class HospitalSurveyController
    {
        private readonly IHospitalSurveyService hospitalSurveyService;

        public HospitalSurveyController(IHospitalSurveyService hospitalSurveyService)
        {
            this.hospitalSurveyService = hospitalSurveyService;
        }

        public void Add(HospitalSurveyDto hospitalSurveyDto)
        {
            hospitalSurveyService.Add(hospitalSurveyDto);
        }

        public List<HospitalSurvey> HospitalSurveys()
        {
            return hospitalSurveyService.HospitalSurveys();
        }

        public int GenerateId()
        {
            return hospitalSurveyService.GenerateId();
        }

        public HospitalSurvey FindById(int id)
        {
            return hospitalSurveyService.FindById(id);
        }

        public void Serialize()
        {
            hospitalSurveyService.Serialize();
        }

        public Dictionary<string, decimal> FindAverageRatingByCategory()
        {
            return hospitalSurveyService.FindAverageRatingByCategory();
        }

        public Dictionary<int, int> FindRatingAppearancesForServiceQuality()
        {
            return hospitalSurveyService.FindRatingAppearancesForServiceQuality();
        }

        public Dictionary<int, int> FindRatingAppearancesForHygiene()
        {
            return hospitalSurveyService.FindRatingAppearancesForHygiene();
        }

        public Dictionary<int, int> FindRatingAppearancesForSatisfaction()
        {
            return hospitalSurveyService.FindRatingAppearancesForSatisfaction();
        }

        public Dictionary<int, int> FindRatingAppearancesForRecommendation()
        {
            return hospitalSurveyService.FindRatingAppearancesForRecommendation();
        }

        public List<string> GetAllComments()
        {
            return hospitalSurveyService.GetAllComments();
        }

    }
}
