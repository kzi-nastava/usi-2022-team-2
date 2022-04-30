using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DoctorSurveyController
    {
        List<DoctorSurvey> doctorSurveys;
        string path;

        public DoctorSurveyController()
        {
            path = "../../../data/entities/DoctorSurveys.json";
            Load();
        }

        public DoctorSurveyController(string path)
        {
            this.path = path;
            Load();
        }

        internal List<DoctorSurvey> DoctorSurveys { get => doctorSurveys; set => doctorSurveys = value; }

        public string Path { get => path; set => path = value; }

        void Load()
        {
            doctorSurveys = JsonSerializer.Deserialize<List<DoctorSurvey>>(File.ReadAllText(path));
        }

        public DoctorSurvey FindById(int id)
        {
            foreach (DoctorSurvey doctorSurvey in doctorSurveys)
                if (doctorSurvey.Id == id)
                    return doctorSurvey;
            return null;
        }

        public void Serialize()
        {
            string doctorSurveysJson = JsonSerializer.Serialize(doctorSurveys, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, doctorSurveysJson);
        }
    }
}
