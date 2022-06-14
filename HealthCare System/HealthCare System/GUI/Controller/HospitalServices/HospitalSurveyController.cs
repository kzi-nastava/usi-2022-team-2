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

    }
}
