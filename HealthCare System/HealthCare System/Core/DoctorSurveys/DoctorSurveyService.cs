﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthCare_System.Core.DoctorSurveys.Repository;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Core.DoctorSurveys.Model;

namespace HealthCare_System.Core.DoctorSurveys
{
    public class DoctorSurveyService : IDoctorSurveyService
    {
        IDoctorSurveyRepo doctorSurveyRepo;

        public DoctorSurveyService(IDoctorSurveyRepo doctorSurveyRepo)
        {
            this.doctorSurveyRepo = doctorSurveyRepo;
        }

        public IDoctorSurveyRepo DoctorSurveyRepo { get => doctorSurveyRepo; }

        public double FindAverageRatingForDoctor(Doctor doctor)
        {
            int sumOfRatings = 0;
            int numberOfRatings = 0;
            foreach (DoctorSurvey survey in DoctorSurveys())
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

        public List<Doctor> SortDoctorsByRatings(List<Doctor> doctors, SortDirection direction)
        {
            List<Tuple<Doctor, double>> ratings = new();
            foreach (Doctor doctor in doctors)
            {
                ratings.Add(new Tuple<Doctor, double>(doctor, FindAverageRatingForDoctor(doctor)));
            }

            List<Tuple<Doctor, double>> sortedRatings;
            if (direction == SortDirection.DESCENDING)
                sortedRatings = ratings.OrderByDescending(t => t.Item2).ToList();
            else
                sortedRatings = ratings.OrderBy(t => t.Item2).ToList();
            List<Doctor> sortedDoctors = new();
            foreach (Tuple<Doctor, double> tuple in sortedRatings)
            {
                sortedDoctors.Add(tuple.Item1);
            }
            return sortedDoctors;
        }

        public void Add(DoctorSurveyDto doctorSurveyDto)
        {
            DoctorSurvey survey = new(doctorSurveyDto);
            doctorSurveyRepo.Add(survey);
        }

        public List<DoctorSurvey> DoctorSurveys()
        {
            return doctorSurveyRepo.DoctorSurveys;
        }
        public DoctorSurvey FindById(int id)
        {
            return doctorSurveyRepo.FindById(id);
        }
        public int GenerateId()
        {
            return doctorSurveyRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv")
        {
            doctorSurveyRepo.Serialize();
        }
    }
}
