﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Model;
using System.IO;
using System.Text.Json;

namespace HealthCare_System.Repository.SurveyRepo
{
    class DoctorSurveyRepo
    {
        List<DoctorSurvey> doctorSurveys;
        string path;

        public DoctorSurveyRepo()
        {
            path = "../../../data/entities/DoctorSurveys.json";
            Load();
        }

        public DoctorSurveyRepo(string path)
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
