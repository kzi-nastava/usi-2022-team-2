﻿using HealthCare_System.Core.DoctorSurveys.Model;

namespace HealthCare_System.Core.DoctorSurveys.Repository
{
    public interface IDoctorSurveyRepo
    {
        string Path { get; set; }

        void Add(DoctorSurvey survey);

        DoctorSurvey FindById(int id);

        int GenerateId();

        void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv");
    }
}