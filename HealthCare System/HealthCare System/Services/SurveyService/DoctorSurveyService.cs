using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.SurveyRepo;

namespace HealthCare_System.Services.SurveyService
{
    class DoctorSurveyService
    {
        DoctorSurveyRepo doctorSurveyRepo;

        public DoctorSurveyService()
        {
            DoctorSurveyRepoFactory doctorSurveyRepoFactory = new();
            doctorSurveyRepo = doctorSurveyRepoFactory.CreateDoctorSurveyRepository();
        }

        public DoctorSurveyRepo DoctorSurveyRepo { get => doctorSurveyRepo;}

        public double FindAverageRatingForDoctor(Doctor doctor)
        {
            int sumOfRatings = 0;
            int numberOfRatings = 0;
            foreach (DoctorSurvey survey in doctorSurveys)
            {
                if (survey.Doctor == doctor)
                {
                    sumOfRatings += survey.ServiceQuality + survey.Recommendation;
                    numberOfRatings += 2;
                }
            }
            if (numberOfRatings != 0)
                return sumOfRatings / numberOfRatings;
            return 0;
        }
    }
}
