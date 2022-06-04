using HealthCare_System.Model;
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
        public int GenerateId()
        {
            return doctorSurveys[^1].Id + 1;
        }

        public double FindAverageRatingForDoctor(Doctor doctor)
        {
            int sumOfRatings = 0;
            int numberOfRatings = 0;
            foreach (DoctorSurvey survey in doctorSurveys)
            {
                if (survey.Doctor == doctor)
                {
                    sumOfRatings += survey.ServiceQuality + survey.Recommendation;
                    numberOfRatings += 2;
                }
            }
            if (numberOfRatings != 0)
                return sumOfRatings / numberOfRatings;
            return 0;
        }

        public void Add(DoctorSurvey survey)
        {
            doctorSurveys.Add(survey);
            Serialize();
        }

        public void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv")
        {
            string doctorSurveysJson = JsonSerializer.Serialize(doctorSurveys, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, doctorSurveysJson);
            string csv = "";
            foreach (DoctorSurvey survey in doctorSurveys)
            {

                csv += survey.Doctor.Jmbg.ToString() + ";" + survey.Id.ToString() + "\n";
            }
            File.WriteAllText(linkPath, csv);
        }
    }
}
