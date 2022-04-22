using HealthCare_System.entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.controllers
{
    class DoctorSurveyController
    {
        List<DoctorSurvey> doctorSurveys;

        public DoctorSurveyController()
        {
            Load();
        }

        internal List<DoctorSurvey> DoctorSurveys { get => doctorSurveys; set => doctorSurveys = value; }

        void Load()
        {
            doctorSurveys = JsonSerializer.Deserialize<List<DoctorSurvey>>(File.ReadAllText("data/entities/DoctorSurveys.json"));
        }

        public DoctorSurvey FindById(int id)
        {
            foreach (DoctorSurvey doctorSurvey in doctorSurveys)
                if (doctorSurvey.Id == id)
                    return doctorSurvey;
            return null;
        }
    }
}
