using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.AppointmentRepo;

namespace HealthCare_System.Services.SchedulingService
{
    class AppointmentRequestService
    {
        AppointmentRequestRepo appointmentRequestRepo;

        public AppointmentRequestService()
        {
            AppointmentRequestRepoFactory appointmentRequestRepoFactory = new();
            appointmentRequestRepo = appointmentRequestRepoFactory.CreateAppointmentRequestRepository();
        }

        internal AppointmentRequestRepo AppointmentRequestRepo { get => appointmentRequestRepo; }
    }
}
