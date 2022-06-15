using HealthCare_System.Core.DoctorSurveys;
using HealthCare_System.Core.DoctorSurveys.Model;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller.DoctorSurveys
{
    class DoctorSurveyController
    {
        private readonly IDoctorSurveyService doctorSurveyService;

        public DoctorSurveyController(IDoctorSurveyService doctorSurveyService)
        {
            this.doctorSurveyService = doctorSurveyService;
        }

        public double FindAverageRatingForDoctor(Doctor doctor)
        {
            return doctorSurveyService.FindAverageRatingForDoctor(doctor);
        }

        public List<Doctor> SortDoctorsByRatings(List<Doctor> doctors, SortDirection direction)
        {
            return doctorSurveyService.SortDoctorsByRatings(doctors, direction);
        }

        public void Add(DoctorSurveyDto doctorSurveyDto)
        {
            doctorSurveyService.Add(doctorSurveyDto);
        }

        public List<DoctorSurvey> DoctorSurveys()
        {
            return doctorSurveyService.DoctorSurveys();
        }
        public DoctorSurvey FindById(int id)
        {
            return doctorSurveyService.FindById(id);
        }
        public int GenerateId()
        {
            return doctorSurveyService.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/Doctor_DoctorSurvey.csv")
        {
            doctorSurveyService.Serialize(linkPath);
        }
    }
}
