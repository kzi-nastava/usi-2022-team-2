using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.Repository.SurveyRepo
{
    class DoctorSurveyRepoFDactory
    {
        private DoctorSurveyRepo doctorSurveyRepo;

        public DoctorSurveyRepo CreateDoctorSurveyRepository()
        {
            if (doctorSurveyRepo == null)
                doctorSurveyRepo = new DoctorSurveyRepo();

            return doctorSurveyRepo;
        }
    }
}
