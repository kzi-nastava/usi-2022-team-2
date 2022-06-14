using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.GUI.Controller
{
    class SchedulingController
    {
        private readonly SchedulingService schedulingService;

        public SchedulingController(SchedulingService schedulingService)
        {
            this.schedulingService = schedulingService;
        }

        public Room AvailableRoom(AppointmentType type, DateTime start, DateTime end)
        {
            return schedulingService.AvailableRoom(type, start, end);
        }

        public Appointment AddAppointment(AppointmentDto appointmentDto)
        {
            return schedulingService.AddAppointment(appointmentDto);

        }

        public void UpdateAppointment(AppointmentDto newAppointmentDto)
        {
            schedulingService.UpdateAppointment(newAppointmentDto);
        }

        public void DeleteAppointment(int id)
        {
            schedulingService.DeleteAppointment(id);
        }

        public Appointment BookAppointmentByReferral(Referral referral)
        {
            return schedulingService.BookAppointmentByReferral(referral);
        }

        public void DeleteAppointmens(Patient patient)
        {
            schedulingService.DeleteAppointmens(patient);
        }
    }
}
