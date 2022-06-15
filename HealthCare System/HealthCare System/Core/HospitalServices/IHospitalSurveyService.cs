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

        public Dictionary<string, decimal> FindAverageRatingByCategory();

        public Dictionary<int, int> FindRatingAppearancesForServiceQuality();

        public Dictionary<int, int> FindRatingAppearancesForHygiene();

        public Dictionary<int, int> FindRatingAppearancesForSatisfaction();

        public Dictionary<int, int> FindRatingAppearancesForRecommendation();

        public List<string> GetAllComments();
    }
}