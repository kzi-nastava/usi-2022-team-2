﻿using HealthCare_System.Core.DoctorSurveys.Model;
using HealthCare_System.Core.DoctorSurveys.Repository;
using HealthCare_System.Core.Users.Model;
using System.Collections.Generic;

namespace HealthCare_System.Core.DoctorSurveys
{
    public interface IDoctorSurveyService
    {
        DoctorSurveyRepo DoctorSurveyRepo { get; }

        void Add(DoctorSurveyDto doctorSurveyDto);

        double FindAverageRatingForDoctor(Doctor doctor);

        List<Doctor> SortDoctorsByRatings(List<Doctor> doctors, SortDirection direction);
    }
}