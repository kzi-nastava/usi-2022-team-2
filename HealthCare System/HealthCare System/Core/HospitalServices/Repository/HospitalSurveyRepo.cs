using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Core.HospitalSurveys.Repository
{
    public class HospitalSurveyRepo
    {
        List<HospitalSurvey> hospitalSurveys;
        string path;

        public HospitalSurveyRepo()
        {
            path = "../../../data/entities/HospitalSurveys.json";
            Load();
        }

        public HospitalSurveyRepo(string path)
        {
            this.path = path;
            Load();
        }

        internal List<HospitalSurvey> HospitalSurveys { get => hospitalSurveys; set => hospitalSurveys = value; }

        public string Path { get => path; set => path = value; }

        public int GenerateId()
        {
            return hospitalSurveys[^1].Id + 1;
        }
        void Load()
        {
            hospitalSurveys = JsonSerializer.Deserialize<List<HospitalSurvey>>(File.ReadAllText(path));
        }

        public void Add(HospitalSurvey survey)
        {
            hospitalSurveys.Add(survey);
            Serialize();
        }

        public HospitalSurvey FindById(int id)
        {
            foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                if (hospitalSurvey.Id == id)
                    return hospitalSurvey;
            return null;
        }

        public void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv")
        {
            string hospitalSurveysJson = JsonSerializer.Serialize(hospitalSurveys,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, hospitalSurveysJson);
        }
    }
}
