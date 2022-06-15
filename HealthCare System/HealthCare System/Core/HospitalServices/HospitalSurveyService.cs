using HealthCare_System.Core.HospitalSurveys.Model;
using HealthCare_System.Core.HospitalSurveys.Repository;
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
    }
}
