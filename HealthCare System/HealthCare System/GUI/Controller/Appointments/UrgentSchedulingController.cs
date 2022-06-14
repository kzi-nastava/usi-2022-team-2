using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Model.Core.Appointments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller.Appointments
{
    class UrgentSchedulingController
    {
        private readonly UrgentSchedulingService urgentSchedulingService;

        public UrgentSchedulingController(UrgentSchedulingService urgentSchedulingService)
        {
            this.urgentSchedulingService = urgentSchedulingService;
        }
        public Appointment ReplaceAppointment(Appointment toReplaceAppointment, UrgentAppointmentDto urgentAppointmentDto)
        {
            return urgentSchedulingService.ReplaceAppointment(toReplaceAppointment, urgentAppointmentDto);
        }

        public void BookClosestEmergancyAppointment(UrgentAppointmentDto urgentAppointmentDto)
        {

            urgentSchedulingService.BookClosestEmergancyAppointment(urgentAppointmentDto);
        }

        public Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration,
            Patient patient)
        {

            return urgentSchedulingService.GetReplaceableAppointments(doctors, duration, patient);
        }
    }
}
