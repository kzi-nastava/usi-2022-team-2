using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.SurveyRepo
{
    class HospitalSurveyRepoFactory
    {
        private HospitalSurveyRepo hospitalSurveyRepo;

        public HospitalSurveyRepo CreateHospitalSurveyRepository()
        {
            if (hospitalSurveyRepo == null)
                hospitalSurveyRepo = new HospitalSurveyRepo();

            return hospitalSurveyRepo;
        }
    }
}
