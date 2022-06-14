using HealthCare_System.Core.HospitalSurveys.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.HospitalSurveys.Repository
{
    public interface IHospitalSurveyRepo
    {
        string Path { get; set; }

        List<HospitalSurvey> HospitalSurveys { get; set; }
        void Add(HospitalSurvey survey);

        HospitalSurvey FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv");
    }
}