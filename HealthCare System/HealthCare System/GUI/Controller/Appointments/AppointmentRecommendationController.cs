using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller.Appointments
{
    class AppointmentRecommendationController
    {
        IAppointmentRecomendationService appointmentRecomendationService;

        public AppointmentRecommendationController(IAppointmentRecomendationService appointmentRecomendationService)
        {
            this.appointmentRecomendationService = appointmentRecomendationService;
        }

        public List<Appointment> RecommendAppointment(DateTime end, int[] from, int[] to, Doctor doctor,
            bool priorityDoctor, Patient patient)
        {
            return appointmentRecomendationService.RecommendAppointment(end, from, to, doctor, priorityDoctor, patient);

        }
    }
}
