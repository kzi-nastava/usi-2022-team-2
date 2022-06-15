using HealthCare_System.Core.HospitalSurveys.Model;
using HealthCare_System.Core.HospitalSurveys.Repository;
using System;
using System.Collections.Generic;

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

        public List<HospitalSurvey> HospitalSurveys()
        {
            return hospitalSurveyRepo.HospitalSurveys;
        }

        public int GenerateId()
        {
            return hospitalSurveyRepo.GenerateId();
        }

        public HospitalSurvey FindById(int id)
        {
            return hospitalSurveyRepo.FindById(id);
        }

        public void Serialize()
        {
            hospitalSurveyRepo.Serialize();
        }

        public Dictionary<string, decimal> FindAverageRatingByCategory()
        {
            Dictionary<string, decimal> averageRatings = new();
            averageRatings["serviceQuality"] = 0;
            averageRatings["hygiene"] = 0;
            averageRatings["satisfaction"] = 0;
            averageRatings["recommendation"] = 0;

            int numberOfRatings = 0;
            int sumOfRatingsServiceQuality = 0;
            int sumOfRatingsHygiene = 0;
            int sumOfRatingsSatisfaction = 0;
            int sumOfRatingsRecommendation = 0;

            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                sumOfRatingsServiceQuality += hospitalSurvey.ServiceQuality;
                sumOfRatingsHygiene += hospitalSurvey.Hygiene;
                sumOfRatingsSatisfaction += hospitalSurvey.Satisfaction;
                sumOfRatingsRecommendation += hospitalSurvey.Recommendation;
                numberOfRatings += 1;
            }

            if (numberOfRatings != 0)
            {
                averageRatings["serviceQuality"] = Math.Round((decimal)((float)sumOfRatingsServiceQuality / numberOfRatings),0);
                averageRatings["hygiene"] = Math.Round((decimal)((float)sumOfRatingsHygiene / numberOfRatings), 0);
                averageRatings["satisfaction"] = Math.Round((decimal)((float)sumOfRatingsSatisfaction / numberOfRatings), 0);
                averageRatings["recommendation"] = Math.Round((decimal)((float)sumOfRatingsRecommendation / numberOfRatings), 0);
            }

            return averageRatings;
        }

        public Dictionary<int, int> FindRatingAppearancesForServiceQuality()
        {
            Dictionary<int, int> serviceQualityRatings = new();
            serviceQualityRatings[5] = 0;
            serviceQualityRatings[4] = 0;
            serviceQualityRatings[3] = 0;
            serviceQualityRatings[2] = 0;
            serviceQualityRatings[1] = 0;
            serviceQualityRatings[0] = 0;

            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                serviceQualityRatings[hospitalSurvey.ServiceQuality] += 1;
            }

            return serviceQualityRatings;

        }

        public Dictionary<int, int> FindRatingAppearancesForHygiene()
        {
            Dictionary<int, int> hygieneRatings = new();
            hygieneRatings[5] = 0;
            hygieneRatings[4] = 0;
            hygieneRatings[3] = 0;
            hygieneRatings[2] = 0;
            hygieneRatings[1] = 0;
            hygieneRatings[0] = 0;

            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                hygieneRatings[hospitalSurvey.Hygiene] += 1;
            }

            return hygieneRatings;

        }

        public Dictionary<int, int> FindRatingAppearancesForSatisfaction()
        {
            Dictionary<int, int> satisfactionRatings = new();
            satisfactionRatings[5] = 0;
            satisfactionRatings[4] = 0;
            satisfactionRatings[3] = 0;
            satisfactionRatings[2] = 0;
            satisfactionRatings[1] = 0;
            satisfactionRatings[0] = 0;

            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                satisfactionRatings[hospitalSurvey.Satisfaction] += 1;
            }

            return satisfactionRatings;

        }

        public Dictionary<int, int> FindRatingAppearancesForRecommendation()
        {
            Dictionary<int, int> recommendationRatings = new();
            recommendationRatings[5] = 0;
            recommendationRatings[4] = 0;
            recommendationRatings[3] = 0;
            recommendationRatings[2] = 0;
            recommendationRatings[0] = 0;

            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                recommendationRatings[hospitalSurvey.Recommendation] += 1;
            }

            return recommendationRatings;

        }

        public List<string> GetAllComments()
        {
            List<string> comments = new();
            foreach (HospitalSurvey hospitalSurvey in HospitalSurveys())
            {
                comments.Add(hospitalSurvey.Comment);
            }
            return comments;
        }
    }
}
