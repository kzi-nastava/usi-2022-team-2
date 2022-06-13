using HealthCare_System.Core.HospitalSurveys.Model;

namespace HealthCare_System.Core.HospitalSurveys.Repository
{
    public interface IHospitalSurveyRepo
    {
        string Path { get; set; }

        void Add(HospitalSurvey survey);

        HospitalSurvey FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv");
    }
}