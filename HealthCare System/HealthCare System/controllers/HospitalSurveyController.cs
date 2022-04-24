using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class HospitalSurveyController
    {
        List<HospitalSurvey> hospitalSurveys;
        string path;

        public HospitalSurveyController()
        {
            path = "data/entities/HospitalSurveys.json";
            Load();
        }

        public HospitalSurveyController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<HospitalSurvey> HospitalSurveys { get => hospitalSurveys; set => hospitalSurveys = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            hospitalSurveys = JsonSerializer.Deserialize<List<HospitalSurvey>>(File.ReadAllText(path));
        }

        public HospitalSurvey FindById(int id)
        {
            foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                if (hospitalSurvey.Id == id)
                    return hospitalSurvey;
            return null;
        }

        public void Serialize()
        {
            string hospitalSurveysJson = JsonSerializer.Serialize(hospitalSurveys, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, hospitalSurveysJson);
        }
    }
}
