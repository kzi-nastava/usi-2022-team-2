using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.SurveyRepo;

namespace HealthCare_System.Services.SurveyService
{
    class HospitalSurveyService
    {
        HospitalSurveyRepo hospitalSurveyRepo;

        public HospitalSurveyService()
        {
            HospitalSurveyRepoFactory hospitalSurveyRepoFactory = new();
            hospitalSurveyRepo = hospitalSurveyRepoFactory.CreateHospitalSurveyRepository();
        }

        public HospitalSurveyRepo HospitalSurveyRepo { get => hospitalSurveyRepo; }
    }
}
