﻿using HealthCare_System.Core.DoctorSurveys.Model;
using HealthCare_System.Core.DoctorSurveys.Repository;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DoctorSurveys
{
    public interface IDoctorSurveyService
    {
        IDoctorSurveyRepo DoctorSurveyRepo { get; }

        void Add(DoctorSurveyDto doctorSurveyDto);

        double FindAverageRatingForDoctor(Doctor doctor);

        List<Doctor> SortDoctorsByRatings(List<Doctor> doctors, SortDirection direction);

        Dictionary<string, decimal> FindAverageRatingForDoctorByCategory(Doctor doctor);

        Dictionary<int, int> FindRatingAppearancesForServiceQuality(Doctor doctor);

        Dictionary<int, int> FindRatingAppearancesForRecommendation(Doctor doctor);

        List<string> GetAllComments(Doctor doctor);

        public List<DoctorSurvey> DoctorSurveys();

        public DoctorSurvey FindById(int id);

        public int GenerateId();

        public void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv");
    }
}