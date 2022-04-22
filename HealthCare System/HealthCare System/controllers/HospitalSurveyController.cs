using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class HospitalSurveyController
    {
        List<HospitalSurvey> hospitalSurveys;

        public HospitalSurveyController()
        {
            Load();
        }

        internal List<HospitalSurvey> HospitalSurveys { get => hospitalSurveys; set => hospitalSurveys = value; }

        void Load()
        {
            hospitalSurveys = JsonSerializer.Deserialize<List<HospitalSurvey>>(File.ReadAllText("data/entities/HospitalSurvey.json"));
        }

        public HospitalSurvey FindById(int id)
        {
            foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                if (hospitalSurvey.Id == id)
                    return hospitalSurvey;
            return null;
        }
    }
}
